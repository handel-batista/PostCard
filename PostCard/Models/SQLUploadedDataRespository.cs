using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PostCard.Models
{
    public class SQLUploadedDataRespository : IUploadedDataRespository
    {
        private readonly AppDbContext context;

        public SQLUploadedDataRespository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// searches the database with the provided parameter 
        /// if an entry is found, delete the matching data and return the deleted data
        /// </summary>
        /// <param name="DataId"></param>
        /// <returns></returns>
        public UploadedData DeleteUploadedData(int DataId)
        {
            UploadedData uploadedData = context.UploadedDatas.Find(DataId);
            if (uploadedData != null)
            {
                context.UploadedDatas.Remove(uploadedData);
                context.SaveChanges();
            }
            return uploadedData;
        }

        /// <summary>
        /// gets the validated data from UploadedData and saves it; returns the saved data
        /// </summary>
        /// <param name="uploadedData"></param>
        /// <returns></returns>
        public UploadedData UploadData(UploadedData uploadedData)
        {
            context.UploadedDatas.Add(uploadedData);
            context.SaveChanges();
            return uploadedData;
        }

        /// <summary>
        /// With this action we pull all the data from the database matching the email
        /// parameter. When the data is retrived, we store the iQuery result to a list and 
        /// convert the list to a data table using the ListToDataTable call. 
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        DataTable IUploadedDataRespository.GetUploadedData(string Email)
        {
            var data = context.UploadedDatas.Where(d => d.Email == Email)
                                            .Select(d => new {
                                                d.DataId, d.Name, d.Email, d.SentDate, d.ImageName
                                            }).ToList();
            return ListToDataTable(data);
        }

        /// <summary>
        /// Converts a list to a datatable using System.Reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
