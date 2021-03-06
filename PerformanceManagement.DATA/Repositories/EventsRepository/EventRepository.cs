﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.EventsRepository
{
    public class EventRepository : IEventRepository
    {


        private PerformanceManagementDBContext _context;

        private IBadgeRepository _badgeRepository;
        public EventRepository(PerformanceManagementDBContext context,IBadgeRepository badgerepository)
        {
            

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _badgeRepository = badgerepository;
           
        }
       
        
                
        
        public void addevent()
        {
            var ev = new Event() ;
           
           bool ver = verifybadge();
            if( ver == true)
            {
                _context.Events.Add(ev);
            }
        }

        public List<DayEvent> returndayeventlist(DayEvent dv)
        {
            List<DayEvent> lv = new List<DayEvent>();
            lv.Add(dv);
            _context.SaveChanges();
            return lv;

        }

      
        public bool CreateDayEvent(DayEvent dv)
        {

            _context.DayEvents.Add(dv);

            return _context.SaveChanges() >= 0 ? true : false; ;


        }

        public bool verifybadge()
        {

            if (_badgeRepository.badgefortoday(DateTime.Today))
                return true;
            else return false;
        }
        public IEnumerable<DayEvent> DayEvents()
        {
          var  events=  _context.Events;
           
            IEnumerable<DayEvent> dayevents =null;
            foreach(var day in events)
            {
                dayevents = day.DayEvent;
            }
            return dayevents;
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        public IEnumerable<Event> Eventsperday(DateTime dateoftoday)
        {
           
            var events = _context.Events.Where(d => d.Date == dateoftoday);

            return events;

        }
       
        public IEnumerable<DayEvent> getAllDayEventsForToday()
        {

            IEnumerable<Event> events = _context.Events.ToList();
            IEnumerable<DayEvent> dayevents = _context.DayEvents.ToList();

            //foreach (var ev in events)
            //{
            //    if (ev.Date == DateTime.Today)
            //    {
            //        return dayevents = ev.DayEvent.OrderByDescending(ev=>ev.Date);
                    
            //    }
            //}
            return dayevents.OrderByDescending(d=>d.Date);
        }

        public String dayevents()
        {

            IEnumerable<DayEvent> dayevents = _context.DayEvents;
            IEnumerable<Event> events=_context.Events;
         
            String titreevent = null;
           DateTime date = new DateTime();
           //foreach(var ev in events)
           // {
           //     date = ev.Date;
           //     if(date == DateTime.Today)
           //     {
           //         foreach(var dv in dayevents)
           //         {
           //            titreevent= dv.Title;
           //         }

                  
           //     }
             
           // }
            return titreevent;
        }

        public void createeventeveryday()
        {
            Event ev = new Event()
            {
                Date = DateTime.Today
            };
             _context.Events.Add(ev);
             _context.SaveChanges();    
        }

       
    }
}
