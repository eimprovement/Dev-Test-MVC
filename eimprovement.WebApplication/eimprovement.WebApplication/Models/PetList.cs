using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class PetList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string photoURLs { get; set; }
        public string status { get; set; }
        public string tags { get; set; }
        public List<Pets> viewModel { get; set; }
        public PetList(string id, string name, string category, string photoURLs, string status, string tags)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.photoURLs = photoURLs;
            this.status = status;
            this.tags = tags;


        }
        public PetList(List<Pets> viewModel)
        {
            this.viewModel = viewModel;


        }


        public PetList()
        {



        }


    }
}