using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SchoolManagement.Controllers;
using SchoolManagement.ViewModels.MajorVMs;
using SchoolManagement.Models;
using SchoolManagement;

namespace SchoolManagement.Test
{
    [TestClass]
    public class MajorControllerTest
    {
        private MajorController _controller;
        private string _seed;

        public MajorControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<MajorController>(_seed, "user");
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
            Assert.IsInstanceOfType(rv.Model, typeof(MajorVM));

            MajorVM vm = rv.Model as MajorVM;
            Major v = new Major();
			
            v.MajorCode = "mH280Yr";
            v.MajorName = "5jNsB";
            v.SchoolId = AddSchool();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Major>().FirstOrDefault();
				
                Assert.AreEqual(data.MajorCode, "mH280Yr");
                Assert.AreEqual(data.MajorName, "5jNsB");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Major v = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.MajorCode = "mH280Yr";
                v.MajorName = "5jNsB";
                v.SchoolId = AddSchool();
                context.Set<Major>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(MajorVM));

            MajorVM vm = rv.Model as MajorVM;
            v = new Major();
            v.ID = vm.Entity.ID;
       		
            v.MajorCode = "BzktnsT";
            v.MajorName = "8C2p";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.MajorCode", "");
            vm.FC.Add("Entity.MajorName", "");
            vm.FC.Add("Entity.SchoolId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Major>().FirstOrDefault();
 				
                Assert.AreEqual(data.MajorCode, "BzktnsT");
                Assert.AreEqual(data.MajorName, "8C2p");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Major v = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.MajorCode = "mH280Yr";
                v.MajorName = "5jNsB";
                v.SchoolId = AddSchool();
                context.Set<Major>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(MajorVM));

            MajorVM vm = rv.Model as MajorVM;
            v = new Major();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID,null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Major>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Major v = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.MajorCode = "mH280Yr";
                v.MajorName = "5jNsB";
                v.SchoolId = AddSchool();
                context.Set<Major>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.ID);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Major v1 = new Major();
            Major v2 = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.MajorCode = "mH280Yr";
                v1.MajorName = "5jNsB";
                v1.SchoolId = AddSchool();
                v2.MajorCode = "BzktnsT";
                v2.MajorName = "8C2p";
                v2.SchoolId = v1.SchoolId; 
                context.Set<Major>().Add(v1);
                context.Set<Major>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new Guid[] { v1.ID, v2.ID });
            Assert.IsInstanceOfType(rv.Model, typeof(MajorBatchVM));

            MajorBatchVM vm = rv.Model as MajorBatchVM;
            vm.Ids = new Guid[] { v1.ID, v2.ID };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Major>().Count(), 0);
            }
        }

        private Guid AddSchool()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.SchoolCode = "qfg";
                v.SchoolName = "7Fga7";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
