using IMS.Models.Domain;
using IMS.Models.ViewModels;
using IMS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;


namespace IMS.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminInsuranceController : Controller
    {
        private readonly IClientRepository clientRepository;
        private readonly IInsuranceRepository insuranceRepository;
        private readonly ILogger<AdminInsuranceController> logger;

        public AdminInsuranceController(IClientRepository clientRepository, 
            IInsuranceRepository insuranceRepository,
            ILogger<AdminInsuranceController> logger)
        {
            this.clientRepository = clientRepository;
            this.insuranceRepository = insuranceRepository;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get clients from repository
            var clients = await clientRepository.GetAllAsync();

            var model = new AddInsuranceRequest
            {
                Clients = clients.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddInsuranceRequest addInsuranceRequest)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // Mapping AddInsuranceRequest to Insurance domain model
            var insurance = new Insurance
            {
                InsuranceType = addInsuranceRequest.InsuranceType,
                Sum = addInsuranceRequest.Sum,
                Subject = addInsuranceRequest.Subject,
                ValidFrom = addInsuranceRequest.ValidFrom,
                ValidUntil = addInsuranceRequest.ValidUntil,
                ClientId = addInsuranceRequest.SelectedClientId
            };

            await insuranceRepository.AddAsync(insurance);

            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var insurances = await insuranceRepository.GetAllAsync();

            return View(insurances);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            // Retrieve insurance details from the repository based on the provided id
            var insurance = await insuranceRepository.GetAsync(id);

            if (insurance != null)
            {
                // Map the insurance details to the DetailInsuranceRequest view model
                var detailInsuranceRequest = new DetailInsuranceRequest
                {
                    Id = insurance.Id,
                    InsuranceType = insurance.InsuranceType,
                    Sum = insurance.Sum,
                    Subject = insurance.Subject,
                    ValidFrom = insurance.ValidFrom,
                    ValidUntil = insurance.ValidUntil,
                    ClientId = insurance.ClientId, // Assuming ClientId is needed in the view model
                    Client = insurance.Client // Assuming Client navigation property is needed in the view model
                };

                // Pass the view model to the view
                return View(detailInsuranceRequest);
            }

            // Handle the case where the insurance with the provided id is not found
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var insurance = await insuranceRepository.GetAsync(id);

            if (insurance != null)
            {
                var editInsuranceRequest = new EditInsuranceRequest
                {
                    Id = insurance.Id,
                    InsuranceType = insurance.InsuranceType,
                    Sum = insurance.Sum,
                    Subject = insurance.Subject,
                    ValidFrom = insurance.ValidFrom,
                    ValidUntil = insurance.ValidUntil
                };

                return View(editInsuranceRequest);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditInsuranceRequest editInsuranceRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingInsurance = await insuranceRepository.GetAsync(editInsuranceRequest.Id);

                    if (existingInsurance != null)
                    {
                        existingInsurance.InsuranceType = editInsuranceRequest.InsuranceType;
                        existingInsurance.Sum = editInsuranceRequest.Sum;
                        existingInsurance.Subject = editInsuranceRequest.Subject;
                        existingInsurance.ValidFrom = editInsuranceRequest.ValidFrom;
                        existingInsurance.ValidUntil = editInsuranceRequest.ValidUntil;

                        var updatedInsurance = await insuranceRepository.UpdateAsync(existingInsurance);

                        if (updatedInsurance != null)
                        {
                            // Log success information for debugging
                            logger.LogInformation("Insurance updated successfully. Redirecting to Detail page: {InsuranceId}", updatedInsurance.Id);

                            return RedirectToAction("Detail", new { id = updatedInsurance.Id });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to update the insurance record.");
                        }
                    }
                    else
                    {
                        // Log information for debugging
                        logger.LogWarning("Insurance not found for update: {InsuranceId}", editInsuranceRequest.Id);

                        return NotFound();
                    }
                }
                else
                {
                    // Log validation errors for debugging
                    foreach (var modelStateEntry in ModelState.Values)
                    {
                        foreach (var error in modelStateEntry.Errors)
                        {
                            logger.LogError("Validation Error: {ErrorMessage}", error.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception for debugging
                logger.LogError(ex, "Error updating insurance");
            }

            // If the code reaches here, there was an issue, and the view with errors is returned
            return View(editInsuranceRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditInsuranceRequest editInsuranceRequest)
        {
            // Talk to repository to delete insurance
            var deletedInsurance = await insuranceRepository.DeleteAsync(editInsuranceRequest.Id);

            if (deletedInsurance != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editInsuranceRequest.Id});

            
        }








    }
}
