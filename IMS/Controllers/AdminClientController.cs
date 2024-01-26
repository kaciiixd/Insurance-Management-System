using Azure;
using IMS.Models.Domain;
using IMS.Models.ViewModels;
using IMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminClientController : Controller
    {
        private readonly IClientRepository clientRepository;

        public AdminClientController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClientRequest addClientRequest)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // Mapping AddClientRequest to Client domain model
            var client = new Client
            {
                FirstName = addClientRequest.FirstName,
                LastName = addClientRequest.LastName,
                DateOfBirth = addClientRequest.DateOfBirth,
                ContactNumber = addClientRequest.ContactNumber,
                Email = addClientRequest.Email,
                Address = addClientRequest.Address
            };

            await clientRepository.AddAsync(client);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var clients = await clientRepository.GetAllAsync();

            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = await clientRepository.GetAsync(id);

            if (client != null)
            {
                var editClientRequest = new EditClientRequest
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    DateOfBirth = client.DateOfBirth,
                    ContactNumber = client.ContactNumber,
                    Email = client.Email,
                    Address = client.Address
                };

                return View(editClientRequest);
            }

            // Handle the case where the client with the given id is not found
            return NotFound();
        }





        [HttpPost]
        public async Task<IActionResult> Edit(EditClientRequest editClientRequest)
        {
            var client = new Client
            {
                Id = editClientRequest.Id,
                FirstName = editClientRequest.FirstName,
                LastName = editClientRequest.LastName,
                DateOfBirth = editClientRequest.DateOfBirth,
                ContactNumber = editClientRequest.ContactNumber,
                Email = editClientRequest.Email,
                Address = editClientRequest.Address
            };

            var updatedClient = await clientRepository.UpdateAsync(client);
            if (updatedClient != null)
            {
                // Show success notification
                return RedirectToAction("Detail", new { id = editClientRequest.Id });
            }
            else
            {
                // Show error notification
                // Redirect back to the same Edit view for correction
                return View(editClientRequest);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedClient = await clientRepository.DeleteAsync(id);

            if (deletedClient != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Detail", new { id = id });
        }



        public async Task<IActionResult> Detail(Guid id)
        {
            var client = await clientRepository.GetAsync(id, includeInsurances: true);

            if (client != null)
            {
                var clientDetails = new DetailClientRequest
                {
                    Id = client.Id, // Set the Id property
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    DateOfBirth = client.DateOfBirth,
                    ContactNumber = client.ContactNumber,
                    Email = client.Email,
                    Address = client.Address,
                    Insurances = client.Insurances?.ToList()
                };

                return View(clientDetails);
            }

            // Handle the case where the client with the given id is not found
            return NotFound();
        }


    }
}
