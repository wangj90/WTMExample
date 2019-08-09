using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SchoolManagement.Controllers;
using SchoolManagement.ViewModels.SchoolVMs;
using SchoolManagement.Models;
using SchoolManagement;

namespace SchoolManagement.Test
{
    [TestClass]
    public class SchoolControllerTest
    {
        private SchoolController _controller;
        private string _seed;

        public SchoolControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<SchoolController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolVM));

            SchoolVM vm = rv.Model as SchoolVM;
            School v = new School();
			
            v.SchoolCode = "p6k";
            v.SchoolName = "ko5Ms";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School>().FirstOrDefault();
				
                Assert.AreEqual(data.SchoolCode, "p6k");
                Assert.AreEqual(data.SchoolName, "ko5Ms");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.SchoolCode = "p6k";
                v.SchoolName = "ko5Ms";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolVM));

            SchoolVM vm = rv.Model as SchoolVM;
            v = new School();
            v.ID = vm.Entity.ID;
       		
            v.SchoolCode = "YuH";
            v.SchoolName = "TuRmzGxE";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.SchoolCode", "");
            vm.FC.Add("Entity.SchoolName", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School>().FirstOrDefault();
 				
                Assert.AreEqual(data.SchoolCode, "YuH");
                Assert.AreEqual(data.SchoolName, "TuRmzGxE");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.SchoolCode = "p6k";
                v.SchoolName = "ko5Ms";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolVM));

            SchoolVM vm = rv.Model as SchoolVM;
            v = new School();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID,null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<School>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.SchoolCode = "p6k";
                v.SchoolName = "ko5Ms";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.ID);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            School v1 = new School();
            School v2 = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SchoolCode = "p6k";
                v1.SchoolName = "ko5Ms";
                v2.SchoolCode = "YuH";
                v2.SchoolName = "TuRmzGxE";
                context.Set<School>().Add(v1);
                context.Set<School>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new Guid[] { v1.ID, v2.ID });
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolBatchVM));

            SchoolBatchVM vm = rv.Model as SchoolBatchVM;
            vm.Ids = new Guid[] { v1.ID, v2.ID };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<School>().Count(), 0);
            }
        }


    }
}
