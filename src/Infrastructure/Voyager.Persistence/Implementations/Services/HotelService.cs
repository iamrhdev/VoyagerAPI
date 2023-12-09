using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Voyager.Application.Abstraction.Repositories.IHotelRepositories;
using Voyager.Application.Abstraction.Services;
using Voyager.Application.DTOs.Hotel_DTOs;
using Voyager.Application.Wrappers;
using Voyager.Domain.Entities;
using Voyager.Persistence.Exceptions;

namespace Voyager.Persistence.Implementations.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelWriteRepository _hotelWriteRepository;
        private readonly IHotelReadRepository _hotelReadRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelWriteRepository hotelWriteRepository,
                            IMapper mapper,
                            IHotelReadRepository hotelReadRepository)
        {
            _hotelWriteRepository = hotelWriteRepository;
            _mapper = mapper;
            _hotelReadRepository = hotelReadRepository;
        }
        public async Task CreateAsync(HotelCreateDto hotelCreateDto)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(hotelCreateDto));
            bool hotelExists = await HotelExists(hotelCreateDto);
            if (!hotelExists)
            {
                Hotel newHotel = _mapper.Map<Hotel>(hotelCreateDto);
                await _hotelWriteRepository.AddAsync(newHotel);
                await _hotelWriteRepository.SaveChangeAsync();
                return;
            }
            throw new Exceptions.DuplicateNameException("Hotel with given Hotel Name already exists");
        }
    
        public async Task<PaginatedResult<HotelGetDto>> GetHotelsPaginated([FromQuery] int page, [FromQuery] int pageSize)
        {
            var query = _hotelReadRepository.GetAllByExpressionOrderBy(hotel => hotel.IsDeleted == false, pageSize, (page - 1) * pageSize, hotel => hotel.DateCreated);
            var hotels = await query.ToListAsync();
            var totalCount = await _hotelReadRepository.GetAll().CountAsync();
            var paginatedResult = new PaginatedResult<HotelGetDto>
            {
                Items = _mapper.Map<List<HotelGetDto>>(hotels),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
            return paginatedResult;
        }

        private async Task<bool> HotelExists(HotelCreateDto hotelCreateDto)
        {
            var result = await _hotelReadRepository.GetByExpressionAsync(hotel => hotel.HotelName == hotelCreateDto.HotelName);
            return result != null;
        }
    }
}
