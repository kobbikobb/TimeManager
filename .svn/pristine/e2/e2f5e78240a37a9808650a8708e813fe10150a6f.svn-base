using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManagerLib.Data;
using TimeManagerLib.Model;

namespace TimeManagerTests
{
    [TestClass]
    public class TimeManagerUnitTests
    {
        [TestMethod]
        public void TestSaveProject()
        {
            var rep = new TimeManagerRepository();

            //Delete all data
            foreach (var pro in rep.GetProjects())
            {
                foreach (var cat in rep.GetProjectCategories(pro))
                {
                    foreach (var te in rep.GetCategoryTasks(cat))
                        rep.DeleteTask(te);

                    rep.DeleteCategory(cat);
                }
                rep.DeleteProject(pro);
            }

            var project = new Project() {Name = "TestProject"};
            rep.SaveProject(project);

            Assert.IsTrue(project.Id != 0, "ProjectId var 0");

            var projects = rep.GetProjects();

            Assert.IsTrue(projects.Count == 1, "Vistuð project voru ekki 1");
            Assert.AreEqual(projects.Single().Name, project.Name, "Vistað nafn stemmdi ekki");
        }

        [TestMethod]
        public void TestSaveCategory()
        {
            var rep = new TimeManagerRepository();

            var projects = rep.GetProjects();
            foreach (var pro in projects)
            {
                foreach(var cat in rep.GetProjectCategories(pro))
                    rep.DeleteCategory(cat);
            }

            Assert.IsTrue(projects.Count == 1, "Ekki er hægt að keyra próf á category nema það sé eitt og aðeins eitt project");

            var project = projects.Single();

            var category = new Category(){Name = "TestCategory"};
            category.IdProject = project.Id;
            category.Project = project;
            
            rep.SaveCategory(category);

            Assert.IsTrue(category.Id != 0, "CategoryId var 0");

            var categories = rep.GetProjectCategories(project);

            projects = rep.GetProjects();
            Assert.IsTrue(projects.Count == 1, "Vistuð project voru ekki 1");

            Assert.IsTrue(categories.Count == 1, "Vistuð category voru ekki 1");
            Assert.AreEqual(categories.Single().Name, category.Name, "Vistað nafn stemmdi ekki");
        }

        [TestMethod]
        public void TestSaveTask()
        {
            var rep = new TimeManagerRepository();

            var projects = rep.GetProjects();

            foreach (var pro in projects)
            {
                foreach (var cat in rep.GetProjectCategories(pro))
                {
                    foreach (var te in rep.GetCategoryTasks(cat))
                        rep.DeleteTask(te);
                }
            }

            Assert.IsTrue(projects.Count == 1, "Ekki er hægt að keyra próf á task nema það sé eitt og aðeins eitt project");

            var project = projects.Single();

            var categories = rep.GetProjectCategories(project);

            Assert.IsTrue(categories.Count == 1, "Ekki er hægt að keyra próf á task nema það sé eitt og aðeins eitt project og undir því eins eitt category");

            var category = categories.Single();

            var task = new Task() { Description = "TestDescription" };
            task.IdCategory = category.Id;
            task.Category = category;
            task.Started = DateTime.Now;
            
            rep.SaveTask(task);

            Assert.IsTrue(task.Id != 0, "task var 0");

            projects = rep.GetProjects();
            categories = rep.GetProjectCategories(project);
            Assert.IsTrue(projects.Count == 1, "Vistuð project voru ekki 1");
            Assert.IsTrue(categories.Count == 1, "Vistuð category voru ekki 1");

            var tasks = rep.GetCategoryTasks(categories.Single());

            Assert.IsTrue(tasks.Count == 1, "Vistuð tösk voru ekki 1");

            Assert.AreEqual(tasks.Single().Description, task.Description, "Vistað task description stemmdi ekki");

            var task2 = new Task() { Description = "TestDescription", Started = DateTime.Now };
            task2.Category = new Category() {Name = "TestCategory2", IdProject = project.Id, Project = project};

            rep.SaveTask(task2);

            projects = rep.GetProjects();
            categories = rep.GetProjectCategories(project);

            Assert.IsTrue(projects.Count == 1, "Vistuð project voru ekki 1");
            Assert.IsTrue(categories.Count == 2, "Vistuð category voru ekki 2");
        }

        [TestMethod]
        public void TestSaveTaskNew()
        {
            var rep = new TimeManagerRepository();

            var projects = rep.GetProjects();

            //Delete all data
            foreach (var pro in projects)
            {
                foreach (var cat in rep.GetProjectCategories(pro))
                {
                    foreach (var te in rep.GetCategoryTasks(cat))
                        rep.DeleteTask(te);

                    rep.DeleteCategory(cat);
                }
                rep.DeleteProject(pro);
            }

            var projectNew = new Project() {Name = "test project new"};

            var task = new Task();
            task.Description = "test task description new";
            task.Started = DateTime.Now;
            task.Category = new Category() { Name = "test category new", Project = projectNew };

            rep.SaveTask(task);

            projects = rep.GetProjects();
            Assert.IsTrue(projects.Count == 1, "Project voru fleiri en 1");

            var categories = rep.GetProjectCategories(projects.Single());
            Assert.IsTrue(categories.Count == 1, "Categories voru fleiri en 1");

            var tasks = rep.GetCategoryTasks(categories.Single());
            Assert.IsTrue(tasks.Count == 1, "Tasks voru fleiri en 1");

            var task2 = new Task();
            task2.Description = "test task description new 2";
            task2.Started = DateTime.Now;
            task2.Category = new Category() { Name = "test category new 2", Project = projectNew, IdProject = projectNew.Id };

            rep.SaveTask(task2);

            projects = rep.GetProjects();
            Assert.IsTrue(projects.Count == 1, "Project voru fleiri en 1");

            categories = rep.GetProjectCategories(projects.Single());
            Assert.IsTrue(categories.Count == 2, "Categories voru ekki 2");

            var tasks2 = rep.GetCategoryTasks(task2.Category);
            Assert.IsTrue(tasks2.Count == 1, "Tasks voru fleiri en 1");
        }
    }
}
