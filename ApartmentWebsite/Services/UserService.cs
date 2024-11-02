using System.Threading.Tasks;
using ApartmentWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentWebsite.Services 
{
    public class UserService
    {
        private readonly PRN221_PRJContext _context;

        public UserService(PRN221_PRJContext context)
        {
            _context = context;
        }
        public string GenerateRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 100000000);
            return randomNumber.ToString(); 
        }
    }
}
