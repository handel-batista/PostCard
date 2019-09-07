using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PostCard.Models;
using Newtonsoft.Json;

namespace PostCard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IUploadedDataRespository _uploadedDataRespository;

        public HomeController(IHostingEnvironment hostingEnvironment, IUploadedDataRespository uploadedDataRespository)
        {
            this.hostingEnvironment = hostingEnvironment;
            _uploadedDataRespository = uploadedDataRespository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UploadedDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string filePath = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Base64 != null)
                {
                    try
                    {
                        // The image must be uploaded to the images folder in wwwroot
                        // To get the path of the wwwroot folder we are using the inject
                        // HostingEnvironment service provided by ASP.NET Core
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images\\" + model.Name);

                        //if the the directory doesn't exist
                        Directory.CreateDirectory(uploadsFolder);
                        
                        // To make sure the file name is unique we are appending a new
                        // GUID value and and an underscore to the file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_PostCardImage.png";
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        byte[] imgBytes = Convert.FromBase64String(model.Base64);
                        using (var imageFile = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.Write(imgBytes, 0, imgBytes.Length);
                            imageFile.Flush();
                        }

                        //Saves the provided information on the database.
                        //We store the filepath to delete the image from the server 
                        //at the user's request.
                        UploadedData newDataEntry = new UploadedData
                        {
                            Name = model.Name,
                            Email = model.Email,
                            SentDate = DateTime.Now,
                            ImageName = uniqueFileName,
                            ImagePath = filePath
                        };
                        _uploadedDataRespository.UploadData(newDataEntry);
                        

                        //Sends the email with the select image as an attachement if no errors occur above
                        var fromAddress = new MailAddress("hbpostcard@gmail.com");
                        var toAddress = new MailAddress(model.Email);
                        const string fromPassword = "Postcardem01";
                        string subject = "";
                        if (model.Subject != "")
                        {
                            subject = model.Subject;
                        }
                        else { subject = "Post Card - Image(s)"; }
                        string body = model.EmailBody;

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                        };
                        Attachment attachment = new Attachment(filePath);
                        using (MailMessage message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            message.Attachments.Add(attachment);
                            smtp.Send(message);
                        }

                        ViewBag.CssClassName = "text-success";
                        ViewBag.ServerMessage = "Image successfully sent.";
                        return View("Index");
                    }
                    catch(Exception ex)
                    {
                        //deletes the uploaded file if an error occurs
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        ViewBag.TextClass = "text-danger";
                        ViewBag.Message = ex.Message;
                    }
                }
                else
                {
                    ViewBag.TextClass = "text-danger";
                    ViewBag.Message = "Please select an image.";
                }
                return View();
            }
            else
            {
                ViewBag.TextClass = "text-danger";
                ViewBag.Message = "The form validation failed.";
            }

            return View();
        }

        /// <summary>
        /// This function gets all the sent images when the user provides
        /// a valid emails address. See the IUploadedDataRespository for more details.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetUploadedImages(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    return JsonConvert.SerializeObject(_uploadedDataRespository.GetUploadedData(email));
                }
                else
                {
                    return "";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Deletes the Uploaded row from the tbSentImages table. 
        /// See IUploadedDataRespository for more detials.
        /// </summary>
        /// <param name="DataId"></param>
        /// <returns></returns>
        public string DeleteUploadedData(int DataId)
        {
            try
            {
                if (!string.IsNullOrEmpty(DataId.ToString()))
                {
                    string imagePath = _uploadedDataRespository.DeleteUploadedData(DataId).ImagePath;

                    //if the entry was successfully deleted lets remove the image as well
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    return JsonConvert.SerializeObject("The row was successfully deleted!");
                }
                else
                {
                    return "An internal error occurred. Please contact your administrator. Error: the DataId was not provided.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Returns any errors  
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
