using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRent.Models;
using CarRent.Data;
using CarRent.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CarRent.Controllers
{
    public class CartController : Controller
    {
        private readonly CarDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(CarDbContext context, UserManager<AppUser> userManager)
        {
            _context = context; 
            _userManager = userManager; 
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int carId, DateTime startDate, DateTime endDate)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı yoksa giriş yapmaya yönlendir
            }
            
            var car = await _context.Car.FindAsync(carId);
            if (car == null || !car.Available)
            {
                return RedirectToAction("Index", "Car");
            }

            
        var dailyRate = car.DailyRate;
            var days = (endDate - startDate).Days;
            var totalPrice = days * dailyRate;

            
            var cartItem = new CartItem
            {
                CarId = car.Id,
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                DailyRate = dailyRate,
                TotalPrice = totalPrice
            };

            
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart", "Cart"); 
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            
            var cartItem = await _context.CartItems.FindAsync(id);

            
            if (cartItem == null)
            {
                return RedirectToAction("ViewCart", "Cart");
            }

            
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart", "Cart");
        }

        public async Task<int> CalculateRentPrice(int carId, DateTime startDate, DateTime endDate, int discount, int penalty)
        {
            
            var car = await _context.Car.FindAsync(carId);

            
            if (car == null || !car.Available)
            {
                return 0;
            }

            
            var dailyRate = car.DailyRate;
            var days = (endDate - startDate).Days;
            var totalPrice = days * dailyRate - penalty + discount;

            return totalPrice;
        }

        
        [Authorize]
        public IActionResult Index()
        {
            var cartItems = _context.CartItems
                .Include(ci => ci.Car)
                .GroupBy(ci => ci.UserId)
                .Select(group => group.ToList())
                .ToList();

            var cartItemList = cartItems.SelectMany(group => group.Select(ci => new CartItem
            {
                Car = ci.Car,
                StartDate = ci.StartDate,
                EndDate = ci.EndDate,
                DailyRate = ci.DailyRate,
                Discount = ci.Discount,
                Penalty = ci.Penalty,
                TotalPrice = ci.TotalPrice
            })).ToList();

            return View(cartItemList);
        }

        
                [Authorize]
        public async Task<IActionResult> ViewCart()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı yoksa giriş yapmaya yönlendir
            }

            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Car)
                .ToListAsync(); // Asenkron olarak çağır

            foreach (var item in cartItems)
            {
                item.TotalPrice = (int)CalculateRentalPrice(item);
            }

            var model = cartItems;
            return View(model);
        }


        
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı yoksa giriş yapmaya yönlendir
            }

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            decimal totalPrice = 0;
            foreach (var item in cartItems)
            {
                // item null kontrolü
                if (item != null)
                {
                    item.TotalPrice = (int)CalculateRentalPrice(item);
                    totalPrice += item.TotalPrice;
                }
            }

            var sale = new Sale
            {
                UserId = user.Id,
                SaleDate = DateTime.Now,
                TotalPrice = totalPrice
            };
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                // item null kontrolü
                if (item != null)
                {
                    var car = await _context.Car.FindAsync(item.CarId);
                    // car null kontrolü
                    if (car != null)
                    {
                        car.Available = false;
                    }
                }
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart", "Cart");
        }

        private decimal CalculateRentalPrice(CartItem item)
        {
            var rentalDays = (item.EndDate - item.StartDate).Days + 1;

            
            var basePrice = item.DailyRate * rentalDays;

            
            if (item.Discount > 0)
            {
                basePrice -= (basePrice * item.Discount) / 100;
            }

           
            if (item.Penalty > 0)
            {
                basePrice += item.Penalty;
            }

            return basePrice;
        }
    }
}
