using KCI_SecureModuleCL.Helpers;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCI_SecureModuleCL.Services
{

    public interface IElementService
    {

        IEnumerable<SM_ELEMENT> GetAll();

        IEnumerable<SM_ELEMENT> GetMenu();

        SM_ELEMENT GetById(int id);

        bool Create(SM_ELEMENT element);

        bool Edit(SM_ELEMENT element);

        bool Delete(int id);
        IEnumerable<SM_ELEMENT> GetMenuByRole(int ID_Role, bool Hierarchy = true, bool isApplication = false);
        string GetMenuUrlDefault(int ID_Role, bool Hierarchy = true, bool isApplication = false);
        Task<bool> UpdateRolesElements(int ID_Role, int[] Elements);

        bool DeletesRolesElements(int ID_Role);

        IEnumerable<SM_ELEMENT> GetAllForRolesAndPermission();

        bool RegisterElements(IList<SM_ELEMENT> Elements);

        IEnumerable<SM_ELEMENT> GetTreeView(int ID_Role);


        IEnumerable<SM_ELEMENT_TYPE> GetAllTypes();

       string[] GetElementsNotAuthorized(int iD_Role);
    }

    public class ElementService : IElementService

    {
        private readonly Security_ModuleContext DB;

        private readonly AppSettings _appSettings;

        IList<SM_ELEMENT> MenuCompleto = new List<SM_ELEMENT>();

        private string BaseUrl { get; set; }
        private readonly IConfiguration Configuration;


        public ElementService(IOptions<AppSettings> appSettings, Security_ModuleContext _DB, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            DB = _DB;
            Configuration = configuration;
            BaseUrl = Configuration.GetSection("RecoveryUrl").Value;
            CreateMenuCompleto();
        }

        private void CreateMenuCompleto()
        {

            var App = DB.SM_ELEMENT.FirstOrDefault(a => a.ID_ElementParent == 0);
            if (App != null)
            { MenuCompleto = DB.SM_ELEMENT.ToList().Where(m => m.ID_Type != ElementType.Element).ToList(); }
            else
            { MenuCompleto = DB.SM_ELEMENT.ToList().Where(m => m.ID_Type == ElementType.Menu).ToList(); }

        }

        public bool Create(SM_ELEMENT element)
        {
            try
            {
                // var ElementParent = DB.SM_ELEMENT.SingleOrDefault(a => a.ID_ElementParent == element.);

                //if (ElementParent != null)
                //return false;
                if (element.TX_Url.Substring(0, 1).Equals("/")) {
                    element.TX_Url = element.TX_Url.Remove(0, 1);
                }
                DB.SM_ELEMENT.Add(element);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Edit(SM_ELEMENT element)
        {
            try
            {

                SM_ELEMENT elementEditted = DB.SM_ELEMENT.SingleOrDefault(u => u.ID_Element == element.ID_Element);

                elementEditted.TX_Icon = element.TX_Icon;
                elementEditted.TX_Name = element.TX_Name;
                if (element.TX_Url.Substring(0, 1).Equals("/"))
                {
                    elementEditted.TX_Url = element.TX_Url.Remove(0, 1);
                }
                else {
                    elementEditted.TX_Url = element.TX_Url;
                }
                elementEditted.ID_Type = element.ID_Type;
                elementEditted.ID_ElementParent = element.ID_ElementParent;

                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var element = DB.SM_ELEMENT.FirstOrDefault(x => x.ID_Element == id);
                DB.SM_ELEMENT.Remove(element);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        public IEnumerable<SM_ELEMENT> GetAll()
        {
            return DB.SM_ELEMENT.Include(x => x.ID_TypeNavigation).OrderBy(x => x.TX_Name);
        }

        public IEnumerable<SM_ELEMENT> GetMenu()
        {
            return FlatToHierarchy(MenuCompleto);
        }

        public IEnumerable<SM_ELEMENT> GetMenuByRole(int ID_Role, bool Hierarchy = true, bool isApplication = false)
        {
            IList<SM_ELEMENT> FilteredMenu;

            var Role = DB.SM_ROLE.SingleOrDefault(r => r.ID_Role == ID_Role);

            var App = DB.SM_ELEMENT.SingleOrDefault(a => a.ID_Element == Role.ID_Element && a.ID_ElementParent == 0);

            //if (App != null)
            //{
            //    FilteredMenu = (from Roles in DB.SM_ROLE
            //                    join Elements_Roles in DB.SM_ROLE_ELEMENT on Roles.ID_Role equals Elements_Roles.ID_Role
            //                    join Elements in DB.SM_ELEMENT on Elements_Roles.ID_Element equals Elements.ID_Element
            //                    where (Elements.ID_Type == ElementType.Menu || Elements.ID_Type == ElementType.Application) && 
            //                    //Elements.ID_Element != Role.ID_Element && 
            //                    Elements_Roles.ID_Role == ID_Role
            //                    select Elements).ToList();

            //}
            //else
            //{
            FilteredMenu = (from Roles in DB.SM_ROLE
                            join Elements_Roles in DB.SM_ROLE_ELEMENT on Roles.ID_Role equals Elements_Roles.ID_Role
                            join Elements in DB.SM_ELEMENT on Elements_Roles.ID_Element equals Elements.ID_Element
                            where Elements.ID_Type == ElementType.Menu &&
                            // Elements.ID_Element != Role.ID_Element && 
                            Elements_Roles.ID_Role == ID_Role
                            select Elements).ToList();


            //}
            if (Hierarchy)
            {
                FilteredMenu.ToList().ForEach(x => x.TX_Url = $"{BaseUrl}{x.TX_Url}");
                FilteredMenu = FlatToHierarchy(FilteredMenu);
            }
            return FilteredMenu;
        }

        public string GetMenuUrlDefault(int ID_Role, bool Hierarchy = true, bool isApplication = false)
        {
            //IList<SM_ELEMENT> FilteredMenu;

            var Role = DB.SM_ROLE.SingleOrDefault(r => r.ID_Role == ID_Role);

            var App = DB.SM_ELEMENT.SingleOrDefault(a => a.ID_Element == Role.ID_Element && a.ID_ElementParent == 0);

            //if (App != null)
            //{
            //    FilteredMenu = (from Roles in DB.SM_ROLE
            //                    join Elements_Roles in DB.SM_ROLE_ELEMENT on Roles.ID_Role equals Elements_Roles.ID_Role
            //                    join Elements in DB.SM_ELEMENT on Elements_Roles.ID_Element equals Elements.ID_Element
            //                    where (Elements.ID_Type == ElementType.Menu || Elements.ID_Type == ElementType.Application) && 
            //                    //Elements.ID_Element != Role.ID_Element && 
            //                    Elements_Roles.ID_Role == ID_Role
            //                    select Elements).ToList();

            //}
            //else
            //{
            try
            {
                return (from Roles in DB.SM_ROLE
                                       join Elements_Roles in DB.SM_ROLE_ELEMENT on Roles.ID_Role equals Elements_Roles.ID_Role
                                       join Elements in DB.SM_ELEMENT on Elements_Roles.ID_Element equals Elements.ID_Element
                                       where Elements.ID_Type == ElementType.Menu &&
                                       Elements.BO_Default == true &&
                                       Elements_Roles.ID_Role == ID_Role
                                       select Elements).FirstOrDefault().TX_Url;
            }
            catch (Exception)
            {

                return "/Home";
            }

            //}
            
        }

        public IEnumerable<SM_ELEMENT> GetAuthorizedByRole(int ID_Role, bool Hierarchy = true, bool isApplication = false)
        {
            IList<SM_ELEMENT> FilteredMenu;

            var Role = DB.SM_ROLE.SingleOrDefault(r => r.ID_Role == ID_Role);

            var App = DB.SM_ELEMENT.SingleOrDefault(a => a.ID_Element == Role.ID_Element && a.ID_ElementParent == 0);

            //if (App != null)
            //{
            //    FilteredMenu = (from Roles in DB.SM_ROLE
            //                    join Elements_Roles in DB.SM_ROLE_ELEMENT on Roles.ID_Role equals Elements_Roles.ID_Role
            //                    join Elements in DB.SM_ELEMENT on Elements_Roles.ID_Element equals Elements.ID_Element
            //                    where (Elements.ID_Type == ElementType.Menu || Elements.ID_Type == ElementType.Application) && 
            //                    //Elements.ID_Element != Role.ID_Element && 
            //                    Elements_Roles.ID_Role == ID_Role
            //                    select Elements).ToList();

            //}
            //else
            //{
            FilteredMenu = (from Roles in DB.SM_ROLE
                            join Elements_Roles in DB.SM_ROLE_ELEMENT on Roles.ID_Role equals Elements_Roles.ID_Role
                            join Elements in DB.SM_ELEMENT on Elements_Roles.ID_Element equals Elements.ID_Element
                            where (Elements.ID_Type == ElementType.Menu || Elements.ID_Type == ElementType.Element) &&
                            // Elements.ID_Element != Role.ID_Element && 
                            Elements_Roles.ID_Role == ID_Role
                            select Elements).ToList();
            FilteredMenu.ToList().ForEach(x => x.TX_Url = $"{BaseUrl}{x.TX_Url}");


            //}
            if (Hierarchy)
                FilteredMenu = FlatToHierarchy(FilteredMenu);

            return FilteredMenu;
        }
        private IList<SM_ELEMENT> FlatToHierarchy(IEnumerable<SM_ELEMENT> Menu)
        {
            Menu = Menu.OrderBy(M => M.ID_ElementParent);
            var lookup = new Dictionary<int, SM_ELEMENT>();
            // actual nested collection to return
            var nested = new List<SM_ELEMENT>();

            foreach (SM_ELEMENT item in Menu)
            {
                if (lookup.ContainsKey(item.ID_ElementParent))
                {
                    // add to the parent's child list 
                    lookup[item.ID_ElementParent].SubMenus.Add(item);
                    lookup[item.ID_ElementParent].SubMenus = lookup[item.ID_ElementParent].SubMenus.OrderBy(m => m.TX_Name).ToList();
                }
                else
                {
                    // no parent added yet (or this is the first time)
                    nested.Add(item);
                }
                lookup.Add(item.ID_Element, item);
            }

            return nested.OrderBy(m => m.TX_Name).ToList();
        }

        public SM_ELEMENT GetById(int id)
        {
            var element = DB.SM_ELEMENT.FirstOrDefault(x => x.ID_Element == id);
            return element;
        }

        public IEnumerable<SM_ELEMENT> GetAllForRolesAndPermission()
        {
            return FlatToHierarchy(DB.SM_ELEMENT);
        }

        public async Task<bool> UpdateRolesElements(int ID_Role, int[] Elements)
        {
            try
            {
                IEnumerable<SM_ELEMENT> ElementosPorRole = GetMenuByRole(ID_Role, false);

                if (ElementosPorRole != null)
                    DB.SM_ROLE_ELEMENT.RemoveRange(DB.SM_ROLE_ELEMENT.Where(r => r.ID_Role == ID_Role));

                foreach (var ID_Element in Elements)
                {
                    await DB.SM_ROLE_ELEMENT.AddAsync(new SM_ROLE_ELEMENT {  ID_Role = ID_Role, ID_Element = ID_Element });
                }
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool RegisterElements(IList<SM_ELEMENT> Elements)
        {
            try
            {
               
                Elements = (from ElementsReceived in Elements
                            where !DB.SM_ELEMENT.Select(x => x.TX_Url).ToArray().Contains(ElementsReceived.TX_Url)
                            select ElementsReceived).ToList();
                Elements.ToList().ForEach(cc => cc.ID_Type = ElementType.Element);
                DB.SM_ELEMENT.AddRange(Elements.ToList());
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SM_ELEMENT> GetTreeView(int ID_Role)
        {
            var TreeView = DB.SM_ELEMENT.AsEnumerable();
            var Authorized = GetAuthorizedByRole(ID_Role, false, true).ToList();
            //TreeView = TreeView.Except(Authorized);
            Authorized.ToList().ForEach(cc => cc.BO_Authorized = true);
            foreach (var item in TreeView)
            {
                if (Authorized.Where(e => e.ID_Element == item.ID_Element).SingleOrDefault() != null)
                    item.BO_Authorized = true;
            }


            //TreeView.ToList().InsertRange(0,(Authorized.ToList()));
            return FlatToHierarchy(TreeView);
        }

        public bool DeletesRolesElements(int ID_Role)
        {
            try
            {
                var Elements = DB.SM_ROLE_ELEMENT.Where(r => r.ID_Role == ID_Role);
                DB.SM_ROLE_ELEMENT.RemoveRange(Elements);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<SM_ELEMENT_TYPE> GetAllTypes()
        {
            return DB.SM_ELEMENT_TYPE.AsEnumerable().OrderBy(x => x.TX_Type);
        }

        public string[] GetElementsNotAuthorized(int iD_Role)
        {

            int[] elementsAllowed = GetAuthorizedByRole(iD_Role, false).Select(x=>x.ID_Element).ToArray();
            return DB.SM_ELEMENT.Where(s => !elementsAllowed.Contains(s.ID_Element) && s.ID_Type == ElementType.Element)
                .Select(s => s.TX_Url)
                .ToArray();
        }
    }

}

