using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace PostCard.Models
{
    public interface IUploadedDataRespository
    {
        /// <summary>
        /// Sets our get database fetch function to get the user's 
        /// sent images when he/she provides an email address that was 
        /// previously used.
        /// It returns the user's Name, Email, SentDate, and the sent images.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        DataTable GetUploadedData(string Email);

        /// <summary>
        /// The UploadData takes all the validated data from the UploadedData class
        /// and saves it to the database.
        /// </summary>
        /// <param name="uploadedData"></param>
        /// <returns></returns>
        UploadedData UploadData(UploadedData uploadedData);

        /// <summary>
        /// The DeleteUploadedData deletes an entry from the database when the user 
        /// clicks the delete button on the tbSentImages table.
        /// If the entry is successfully deleted, the Home controller function 
        /// DeleteUploadedData function deletes the image. 
        /// </summary>
        /// <param name="DataId"></param>
        /// <returns></returns>
        UploadedData DeleteUploadedData(int DataId);
    }
}
