using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Reflection;
using DAL;

using System.Data;

namespace BusinessLayer
{
    public class Productions : ProductionEntity, IDisposable, IProductionLogic
    {
        protected DataTable datatable;

        public Productions()
        {

        }

        public Productions(string productionname)
        {
            this.ProductionName = productionname;
            this.GetProduction();

        }

        public void UpdateDB()
        {
            using (ProductionDAL pdal = new ProductionDAL())
            {
                DataFactory.CreateConnection();
                bool Existence = pdal.CheckExistence(this);

                if (Existence==true)
                {
                    pdal.Update(this); 
                }
                else
                {
                    Console.WriteLine("No record found to be updated");
                }
            }
        }


        public Productions GetProduction()
        {
            using (ProductionDAL pdal = new ProductionDAL())
            {
                ProductionEntity newProd = new ProductionEntity();
                //pdal.GetDataTable(this.ProductionName);
                newProd = pdal.RetrieveProductionData(this);

                foreach (PropertyInfo pi in typeof(ProductionEntity).GetProperties())
                {
                    pi.SetValue(this, pi.GetValue(newProd));

                }
                return this;
            }
        }

        public void CreateNew(Productions currProd)
        {
            //Productions newProd = new Productions(currProd.ProductionName);
            using (ProductionDAL pdal = new ProductionDAL())
            {
                
                DataFactory.CreateConnection();
                bool Existence = pdal.CheckExistence(currProd);
                if (Existence== false)
                {
                    pdal.Add(currProd);
                    GetProduction();
                }
                else
                {
                    Console.WriteLine("Record existed already");
                }
                
            }
        }

        public void UpdateProperty(object sender)
        {
            // get type of objects
            Type objType = this.GetType();
            Type senderType = sender.GetType();

            //get object property name from control names
            string propertyName = senderType.GetProperty("Name").GetValue(sender).ToString();
            propertyName = propertyName.Substring(propertyName.IndexOf('_') + 1);

            //retrieve control values 
            var newValue = senderType.GetProperty("Text").GetValue(sender);
            //newValue = string.IsNullOrEmpty((string)newValue) ? string.Empty:newValue;

            //switch target property type
            switch (objType.GetProperty(propertyName).PropertyType.ToString())
            {
                case "System.Int32":
                    if (string.IsNullOrEmpty((string)newValue))
                    {
                        newValue = int.MinValue;
                    }
                    else
                    {
                        try
                        {
                            newValue = int.Parse((string)newValue);
                        }
                        catch (Exception ex)
                        {
                            newValue = int.MinValue;
                            Console.WriteLine(ex.Message);
                        }
                    }
                    break;
                case "System.DateTime":
                    if (string.IsNullOrEmpty((string)newValue))
                    {
                        newValue = DateTime.MinValue;
                    }
                    else
                    {
                        try
                        {
                            newValue = DateTime.Parse((string)newValue);
                        }
                        catch (Exception ex)
                        {
                            newValue = DateTime.MinValue;
                            Console.WriteLine(ex.Message);
                        }
                    }
                    break;
                default:
                    break;
            }

            //set values to current production object
            objType.GetProperty(propertyName).SetValue(this, Convert.ChangeType(newValue, objType.GetProperty(propertyName).PropertyType));
        }
    }

    public interface IProductionLogic
    {
        void UpdateProperty(object sender);
        //Productions CreateNew(Productions currProd);
        //Productions CreateNew(string str);
    }
}
