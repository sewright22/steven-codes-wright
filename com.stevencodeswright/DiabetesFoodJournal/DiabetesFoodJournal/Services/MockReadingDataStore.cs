using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockReadingDataStore : IDataStore<GlucoseReading>
    {
            readonly List<GlucoseReading> readings;

        public MockReadingDataStore()
        {
            readings = new List<GlucoseReading>()
            {
                new GlucoseReading(){Id = 1, Reading = 120, DisplayTime = new DateTime(2020, 02, 16, 1,10,10)},
                new GlucoseReading(){Id = 2, Reading = 122, DisplayTime = new DateTime(2020, 02, 16, 1,15,15)},
                new GlucoseReading(){Id = 3, Reading = 124, DisplayTime = new DateTime(2020, 02, 16, 1,20,20)},
                new GlucoseReading(){Id = 4, Reading = 121, DisplayTime = new DateTime(2020, 02, 16, 1,25,25)},
                new GlucoseReading(){Id = 5, Reading = 118, DisplayTime = new DateTime(2020, 02, 16, 1,30,30)},
                new GlucoseReading(){Id = 6, Reading = 115, DisplayTime = new DateTime(2020, 02, 16, 1,35,35)},
                new GlucoseReading(){Id = 7, Reading = 113, DisplayTime = new DateTime(2020, 02, 16, 1,40,40)},
                new GlucoseReading(){Id = 8, Reading = 111, DisplayTime = new DateTime(2020, 02, 16, 1,45,45)},
                new GlucoseReading(){Id = 9, Reading = 117, DisplayTime = new DateTime(2020, 02, 16, 1,50,50)},
                new GlucoseReading(){Id = 10, Reading = 129, DisplayTime = new DateTime(2020, 02, 16, 1,55,55)},
                new GlucoseReading(){Id = 11, Reading = 139, DisplayTime = new DateTime(2020, 02, 16, 2,0,0)},
                new GlucoseReading(){Id = 12, Reading = 155, DisplayTime = new DateTime(2020, 02, 16, 1,5,5)},
                new GlucoseReading(){Id = 13, Reading = 170, DisplayTime = new DateTime(2020, 02, 16, 1,10,10)},
                new GlucoseReading(){Id = 14, Reading = 182, DisplayTime = new DateTime(2020, 02, 16, 1,15,15)},
                new GlucoseReading(){Id = 15, Reading = 189, DisplayTime = new DateTime(2020, 02, 16, 1,20,20)},
                new GlucoseReading(){Id = 16, Reading = 195, DisplayTime = new DateTime(2020, 02, 16, 1,25,25)},
                new GlucoseReading(){Id = 17, Reading = 199, DisplayTime = new DateTime(2020, 02, 16, 1,30,30)},
                new GlucoseReading(){Id = 18, Reading = 201, DisplayTime = new DateTime(2020, 02, 16, 1,35,35)},
                new GlucoseReading(){Id = 19, Reading = 200, DisplayTime = new DateTime(2020, 02, 16, 1,40,40)},
                new GlucoseReading(){Id = 20, Reading = 203, DisplayTime = new DateTime(2020, 02, 16, 1,45,45)},
                new GlucoseReading(){Id = 21, Reading = 205, DisplayTime = new DateTime(2020, 02, 16, 1,50,50)},
                new GlucoseReading(){Id = 22, Reading = 204, DisplayTime = new DateTime(2020, 02, 16, 1,55,55)},
                new GlucoseReading(){Id = 23, Reading = 206, DisplayTime = new DateTime(2020, 02, 16, 3,0,0)},
                new GlucoseReading(){Id = 24, Reading = 210, DisplayTime = new DateTime(2020, 02, 16, 1,5,5)},
                new GlucoseReading(){Id = 25, Reading = 209, DisplayTime = new DateTime(2020, 02, 16, 1,10,10)},
                new GlucoseReading(){Id = 26, Reading = 214, DisplayTime = new DateTime(2020, 02, 16, 1,15,15)},
                new GlucoseReading(){Id = 27, Reading = 215, DisplayTime = new DateTime(2020, 02, 16, 1,20,20)},
                new GlucoseReading(){Id = 28, Reading = 216, DisplayTime = new DateTime(2020, 02, 16, 1,25,25)},
                new GlucoseReading(){Id = 29, Reading = 217, DisplayTime = new DateTime(2020, 02, 16, 1,30,30)},
                new GlucoseReading(){Id = 30, Reading = 215, DisplayTime = new DateTime(2020, 02, 16, 1,35,35)},
                new GlucoseReading(){Id = 31, Reading = 220, DisplayTime = new DateTime(2020, 02, 16, 1,40,40)},
                new GlucoseReading(){Id = 32, Reading = 229, DisplayTime = new DateTime(2020, 02, 16, 1,45,45)},
                new GlucoseReading(){Id = 33, Reading = 234, DisplayTime = new DateTime(2020, 02, 16, 1,50,50)},
                new GlucoseReading(){Id = 34, Reading = 238, DisplayTime = new DateTime(2020, 02, 16, 1,55,55)},
                new GlucoseReading(){Id = 35, Reading = 242, DisplayTime = new DateTime(2020, 02, 16, 4,0,0)},
                new GlucoseReading(){Id = 36, Reading = 245, DisplayTime = new DateTime(2020, 02, 16, 1,5,5)},
                new GlucoseReading(){Id = 37, Reading = 244, DisplayTime = new DateTime(2020, 02, 16, 1,10,10)},
                new GlucoseReading(){Id = 38, Reading = 243, DisplayTime = new DateTime(2020, 02, 16, 1,15,15)},
                new GlucoseReading(){Id = 39, Reading = 237, DisplayTime = new DateTime(2020, 02, 16, 1,20,20)},
                new GlucoseReading(){Id = 40, Reading = 230, DisplayTime = new DateTime(2020, 02, 16, 1,25,25)},
                new GlucoseReading(){Id = 41, Reading = 224, DisplayTime = new DateTime(2020, 02, 16, 1,30,30)},
                new GlucoseReading(){Id = 42, Reading = 217, DisplayTime = new DateTime(2020, 02, 16, 1,35,35)},
                new GlucoseReading(){Id = 43, Reading = 219, DisplayTime = new DateTime(2020, 02, 16, 1,40,40)},
                new GlucoseReading(){Id = 44, Reading = 215, DisplayTime = new DateTime(2020, 02, 16, 1,45,45)},
                new GlucoseReading(){Id = 45, Reading = 212, DisplayTime = new DateTime(2020, 02, 16, 1,50,50)},
                new GlucoseReading(){Id = 46, Reading = 211, DisplayTime = new DateTime(2020, 02, 16, 1,55,55)},
                new GlucoseReading(){Id = 47, Reading = 209, DisplayTime = new DateTime(2020, 02, 16, 5,0,0)},
                new GlucoseReading(){Id = 48, Reading = 207, DisplayTime = new DateTime(2020, 02, 16, 1,5,5)},
                new GlucoseReading(){Id = 49, Reading = 204, DisplayTime = new DateTime(2020, 02, 16, 1,10,10)},
                new GlucoseReading(){Id = 50, Reading = 195, DisplayTime = new DateTime(2020, 02, 16, 1,15,15)},
                new GlucoseReading(){Id = 51, Reading = 193, DisplayTime = new DateTime(2020, 02, 16, 1,20,20)},
                new GlucoseReading(){Id = 52, Reading = 189, DisplayTime = new DateTime(2020, 02, 16, 1,25,25)},
                new GlucoseReading(){Id = 53, Reading = 188, DisplayTime = new DateTime(2020, 02, 16, 1,30,30)},
                new GlucoseReading(){Id = 54, Reading = 185, DisplayTime = new DateTime(2020, 02, 16, 1,35,35)},
                new GlucoseReading(){Id = 55, Reading = 180, DisplayTime = new DateTime(2020, 02, 16, 1,40,40)},
                new GlucoseReading(){Id = 56, Reading = 181, DisplayTime = new DateTime(2020, 02, 16, 1,45,45)},
                new GlucoseReading(){Id = 57, Reading = 174, DisplayTime = new DateTime(2020, 02, 16, 1,50,50)},
                new GlucoseReading(){Id = 58, Reading = 175, DisplayTime = new DateTime(2020, 02, 16, 1,55,55)},
                new GlucoseReading(){Id = 59, Reading = 176, DisplayTime = new DateTime(2020, 02, 16, 6,0,0)},
                new GlucoseReading(){Id = 60, Reading = 180, DisplayTime = new DateTime(2020, 02, 16, 1,5,5)},
            };                                         
        }                                              
        public Task<bool> AddItemAsync(GlucoseReading item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GlucoseReading> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GlucoseReading>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(readings);
        }

        public Task<bool> UpdateItemAsync(GlucoseReading item)
        {
            throw new NotImplementedException();
        }
    }
}
