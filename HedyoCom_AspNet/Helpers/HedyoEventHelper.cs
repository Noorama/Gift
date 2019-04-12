using HedyoCom_AspNet.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HedyoCom_AspNet.Helpers
{
    public static class HedyoEventHelper
    {
        public static string GetRoleTitle(string eventType, string role)
        {
            if (eventType == "wedding")
            {
                return role == EventConstants.ManRole ? "Damat" : "Gelin";
            }
            else if (eventType == "birthday")
            {
                return role = "Doğan Kişi";
            }

            else if (eventType == "babyborn")
            {
                if (role == EventConstants.SimpleManRole)
                {
                    return role = "Baba";
                }
                else if (role == EventConstants.SimpleWomanRole)
                {
                    return role = "Anne";
                }
                else
                {
                    return role = "Bebek";
                }

            }

            else if (eventType == "new_house_keeping")
            {
                return role = "Evi Tutan";
            }

            else if (eventType == "new_work_open")
            {
                return role = "BusinessMan";
            }

            else if (eventType == "pipi_cuting")
            {
                return role = "Çocuk";
            }
            else if (eventType == "custom")
            {
                return role = "İlgili Kişi";
            }


            return "İlgili Kişi";
        }
    }
}