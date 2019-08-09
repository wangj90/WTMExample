using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SchoolManagement.Models;


namespace SchoolManagement.ViewModels.MajorVMs
{
    public partial class MajorVM : BaseCRUDVM<Major>
    {
        public List<ComboSelectListItem> AllSchools { get; set; }
        public List<ComboSelectListItem> AllStudentMajorss { get; set; }
        [Display(Name = "学生")]
        public List<Guid> SelectedStudentMajorsIDs { get; set; }

        public MajorVM()
        {
            SetInclude(x => x.School);
            SetInclude(x => x.StudentMajors);
        }

        protected override void InitVM()
        {
            AllSchools = DC.Set<School>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.SchoolName);
            AllStudentMajorss = DC.Set<Student>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            SelectedStudentMajorsIDs = Entity.StudentMajors.Select(x => x.StudentId).ToList();
        }

        public override void DoAdd()
        {
            if (SelectedStudentMajorsIDs != null)
            {
                foreach (var id in SelectedStudentMajorsIDs)
                {
                    Entity.StudentMajors.Add(new StudentMajor { StudentId = id });
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            if(SelectedStudentMajorsIDs == null || SelectedStudentMajorsIDs.Count == 0)
            {
                FC.Add("Entity.SelectedStudentMajorsIDs.DONOTUSECLEAR", "true");
            }
            else
            {
                Entity.StudentMajors = new List<StudentMajor>();
                SelectedStudentMajorsIDs.ForEach(x => Entity.StudentMajors.Add(new StudentMajor { ID = Guid.NewGuid(), StudentId = x }));
            }

            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
