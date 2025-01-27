using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearningApp.Data;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> AddEventAsync(Event newEvent)
        {
            try
            {
                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();
                return newEvent;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add the event.", ex);
            }
        }

        public async Task<Event> UpdateEventAsync(Event updatedEvent)
        {
            try
            {
                var existingEvent = await _context.Events.FindAsync(updatedEvent.Id);
                if (existingEvent == null)
                {
                    throw new KeyNotFoundException("Event not found.");
                }

                existingEvent.Title = updatedEvent.Title;
                existingEvent.StartDate = updatedEvent.StartDate;
                existingEvent.EndDate = updatedEvent.EndDate;
                existingEvent.Description = updatedEvent.Description;

                _context.Events.Update(existingEvent);
                await _context.SaveChangesAsync();
                return existingEvent;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update the event.", ex);
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            try
            {
                var eventToDelete = await _context.Events.FindAsync(id);
                if (eventToDelete == null)
                {
                    throw new KeyNotFoundException("Event not found.");
                }

                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to delete the event.", ex);
            }
        }
    }
}
