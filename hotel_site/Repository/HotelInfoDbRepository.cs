﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class HotelInfoDbRepository : IRepository<HotelInfo>
    {
        private DataContext _context;

        public HotelInfoDbRepository(DataContext dataContext)
        {
            _context = dataContext;
            if (_context.HotelInfo.Any()) return;

            //добавление информации об отеле
            HotelInfo hotelInfo = new HotelInfo(1,
                "Восточные воды",
                "Отель был основан в 1969 году и на данный момент является одним из самых популярных.",
                "8-909-998-87-76",
                "eastwater@yandex.ru");

            _context.HotelInfo.Add(hotelInfo);

            _context.SaveChanges();
        }

        public IEnumerable<HotelInfo> GetEntityList()
        {
            return _context.HotelInfo.ToList();
        }

        public HotelInfo GetEntity(int id)
        {
            return _context.HotelInfo.FirstOrDefault(k => k.Id == id);
        }

        public void Create(HotelInfo entity)
        {
            if (_context.HotelInfo.Contains(entity))
                throw new Exception("Ошибка. Данный раздел информации отеля уже существует.");
            _context.HotelInfo.Add(entity);
            _context.SaveChanges();
        }

        public void Update(HotelInfo entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            HotelInfo entity = GetEntity(id);
            if (!_context.HotelInfo.Contains(entity))
                throw new Exception("Ошибка. Данный раздел информации отеля не существует.");
            _context.HotelInfo.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
