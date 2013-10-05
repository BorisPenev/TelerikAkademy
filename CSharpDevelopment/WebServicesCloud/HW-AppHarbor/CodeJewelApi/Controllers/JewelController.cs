using CodeJewelApi.Models;
using CodeJewelData;
using CodeJewelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewelApi.Controllers
{
    public class JewelController : BaseController
    {
       [HttpPost]
       [ActionName("addjewel")]
        public HttpResponseMessage AddCodeJewel([FromBody] CodeJewel code)
       {
           var response = PerformOperation(() =>
               {
                   var context = new CodeContext();
                   using (context)
                   {
                       context.CodeJewels.Add(code);
                       context.SaveChanges();
                       CodeJewelModel model = DesirializeCodeJewel(code);
                       return model;
                   }
               });
           return response;
       }

       [HttpGet]
       [ActionName("getall")]
       public HttpResponseMessage GetAllCodeJewels()
       {
           var response = PerformOperation(() =>
           {
               var context = new CodeContext();
               using (context)
               {
                   var jewels = context.CodeJewels;
                   HashSet<CodeJewelModel> models = new HashSet<CodeJewelModel>();
                   foreach (var codeJewel in jewels)
                   {
                       models.Add(DesirializeCodeJewel(codeJewel));
                   }

                   return models;
               }
           });
           return response;
       }

       [HttpGet]
       [ActionName("bysource")]
       public HttpResponseMessage SearchByCriteriaSource(string source)
       {
           var response = PerformOperation(() =>
           {
               var context = new CodeContext();
               using (context)
               {
                   var jewels = context.CodeJewels.Include("Category").Include("Votes").Where(code=>code.SourceCode.Contains(source));
                   HashSet<CodeJewelModelFull> models = new HashSet<CodeJewelModelFull>();
                   foreach (var codeJewel in jewels)
                   {
                       models.Add(DesirializeCodeJewelToFull(codeJewel));
                   }

                   return models;
               }
           });
           return response;
       }

       [HttpGet]
       [ActionName("bycategory")]
       public HttpResponseMessage SearchByCriteriaCategory(string category)
       {
           var response = PerformOperation(() =>
           {
               var context = new CodeContext();
               using (context)
               {
                   var jewels = context.CodeJewels.Include("Category").Include("Votes").Where(code => code.Category.Name==category);
                   HashSet<CodeJewelModelFull> models = new HashSet<CodeJewelModelFull>();
                   foreach (var codeJewel in jewels)
                   {
                       models.Add(DesirializeCodeJewelToFull(codeJewel));
                   }

                   return models;
               }
           });
           return response;
       }
       private CodeJewelModelFull DesirializeCodeJewelToFull(CodeJewel codeJewel)
       {
           
           double averageVote = 0;
           if (codeJewel.Votes.Count>0)
           {
              averageVote  = codeJewel.Votes.Average(v => v.VoteValue);
           }

           string category = "No Category";
           if (codeJewel.Category!= null)
           {
               category = codeJewel.Category.Name;
           }

           CodeJewelModelFull modelFull = new CodeJewelModelFull
           {
               AuthorName = codeJewel.AuthorMail,
               CodeJewel = codeJewel.SourceCode,
               Id = codeJewel.Id,
               AverageVote = averageVote,
               category = category
           };

           return modelFull;
       }
        
       private CodeJewelModel DesirializeCodeJewel(CodeJewel code)
       {
           CodeJewelModel model = new CodeJewelModel
           {
               AuthorName = code.AuthorMail,
               CodeJewel = code.SourceCode,
               Id = code.Id
           };

           return model;
       }
    }
}
