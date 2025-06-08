using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmployeeAdminPortal.Model.Entities;
using System; // For Exception

public class EmployeesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EmployeesController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET: List all employees
    public async Task<IActionResult> Index()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await client.GetAsync("api/Employees");

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
                return View(employees);
            }

            ViewBag.Error = $"API returned: {response.StatusCode}";
        }
        catch (HttpRequestException ex)
        {
            ViewBag.Error = $"API Connection Failed: {ex.Message}";
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Unexpected Error: {ex.Message}";
        }

        return View(new List<Employee>()); // Return empty list on errors
    }

    // GET: Show "Create Employee" form
    public IActionResult Create()
    {
        return View();
    }

    // POST: Add new employee
    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(employee);

            var client = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await client.PostAsJsonAsync("api/Employees", employee);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = $"API Error: {response.StatusCode}";
        }
        catch (HttpRequestException ex)
        {
            ViewBag.Error = $"API Connection Failed: {ex.Message}";
        }

        return View(employee); // Return to form with error
    }

    // GET: Show "Edit Employee" form
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await client.GetAsync($"api/Employees/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<Employee>();
                return View(employee);
            }

            ViewBag.Error = $"API returned: {response.StatusCode}";
        }
        catch (HttpRequestException ex)
        {
            ViewBag.Error = $"API Connection Failed: {ex.Message}";
        }

        return RedirectToAction("Index");
    }

    // POST: Update existing employee
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, Employee employee)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(employee);

            var client = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await client.PutAsJsonAsync($"api/Employees/{id}", employee);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = $"API Error: {response.StatusCode}";
        }
        catch (HttpRequestException ex)
        {
            ViewBag.Error = $"API Connection Failed: {ex.Message}";
        }

        return View(employee);
    }

    // GET: Show "Delete Confirmation" page
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await client.GetAsync($"api/Employees/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<Employee>();
                return View(employee);
            }

            ViewBag.Error = $"API returned: {response.StatusCode}";
        }
        catch (HttpRequestException ex)
        {
            ViewBag.Error = $"API Connection Failed: {ex.Message}";
        }

        return RedirectToAction("Index");
    }

    // POST: Delete employee
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await client.DeleteAsync($"api/Employees/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = $"API Error: {response.StatusCode}";
        }
        catch (HttpRequestException ex)
        {
            ViewBag.Error = $"API Connection Failed: {ex.Message}";
        }

        return RedirectToAction("Delete", new { id });
    }
}
