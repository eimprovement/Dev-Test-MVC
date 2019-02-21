using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class Pets
    {

        public long id { get; set; }
        public string name { get; set; }
        public Category category { get; set; }
        public string[] photoURLs { get; set; }
        public string status { get; set; }
        public Tags[] tags { get; set; }
        public List<Pets> viewModel { get; set; }
        public Pets(long id, string name, Category category, string[] photoURLs, string status, Tags[] tags)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.photoURLs = photoURLs;
            this.status = status;
            this.tags = tags;


        }
        public Pets(List<Pets> viewModel)
        {
            this.viewModel = viewModel;


        }
        

        public Pets()
        {



        }
    }
}