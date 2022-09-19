using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Krypton_Core.Utils
{
    public class AmmoSortClass
    {
        Api? Api;
        private string? LCB10 { get; set; }
        private string? MCB25 { get; set; }
        private string? MCB50 { get; set; }
        private string? UCB100 { get; set; }
        private string? SAB { get; set; }
        public AmmoSortClass(Api api , string text)
        {
            Api = api;
            GetIDS(text);
            GetLcb10(text);
            GetMcb25(text);
            GetMcb50(text);
            GetUcb100(text);
            GetSab(text);
        }
        

        private void GetIDS(string text)
        {
            var lcb10 = Regex.Match(text,"\"L\":([0-9]+),\"name\":\"LCB-10\"");
            if(lcb10.Success)
            {
                LCB10 = lcb10.Groups[1].Value;
            }
            var mcb25 = Regex.Match(text,"\"L\":([0-9]+),\"name\":\"MCB-25\"");
            if(mcb25.Success)
            {
                MCB25 = mcb25.Groups[1].Value;
            }
            var mcb50 = Regex.Match(text,"\"L\":([0-9]+),\"name\":\"MCB-50\"");
            if(mcb50.Success)
            {
                MCB50 = mcb50.Groups[1].Value;
            }
            var ucb100 = Regex.Match( text, "\"L\":([0-9]+),\"name\":\"UCB-100\",");
            if(ucb100.Success)
            {
                UCB100 = ucb100.Groups[1].Value;
            }
                

            var sab = Regex.Match(text , "\"L\":([0-9]+),\"name\":\"SAB-50\",\"T\":(.*),\"C\"");
            if(sab.Success)
            {
                SAB = sab.Groups[1].Value;
            }
            
        }


        private void GetLcb10(string text)
        {

            var match = Regex.Match(text, "\"I\":\"(.*?)\",\"LV\":(.?),\"L\":" + $"{LCB10}"  + ",\"EQH\":(.*?),\"EQC\":(.*?),\"EQT\":(.*?),\"Q\":([0-9]+)");
            if(match.Success)
            {
              
                Api._user.Ammo.LCB10 = double.Parse(match.Groups[6].Value);
                Api._user.Ammo.Ammos.Add(new("LCB-10", Api._user.Ammo.LCB10));

            }
       
                
            

            
        }
        private void GetMcb25(string text)
        {

            var match = Regex.Match(text, "\"I\":\"(.*?)\",\"LV\":(.?),\"L\":" + $"{MCB25}" + ",\"EQH\":(.*?),\"EQC\":(.*?),\"EQT\":(.*?),\"Q\":([0-9]+)");
            if (match.Success)
            {
              
                Api._user.Ammo.MCB25 = double.Parse(match.Groups[6].Value);
                Api._user.Ammo.Ammos.Add(new("MCB-25" , Api._user.Ammo.MCB25));

            }
            else
            {
               
            }
        }
        private void GetMcb50(string text)
        {

            var match = Regex.Match(text, "\"I\":\"(.*?)\",\"LV\":(.?),\"L\":" + $"{MCB50}" + ",\"EQH\":(.*?),\"EQC\":(.*?),\"EQT\":(.*?),\"Q\":([0-9]+)");
            if (match.Success)
            {
                
                Api._user.Ammo.MCB50 = double.Parse(match.Groups[6].Value);
                Api._user.Ammo.Ammos.Add(new("MCB-50", Api._user.Ammo.MCB50));

            }
           
        }
        private void GetUcb100(string text)
        {

            var match = Regex.Match(text, "\"I\":\"(.*?)\",\"LV\":(.?),\"L\":" + $"{UCB100}" + ",\"EQH\":(.*?),\"EQC\":(.*?),\"EQT\":(.*?),\"Q\":([0-9]+)");
            if (match.Success)
            {
                
                Api._user.Ammo.UCB100 = double.Parse(match.Groups[6].Value);
                Api._user.Ammo.Ammos.Add(new("UCB-100", Api._user.Ammo.UCB100));

            }
      
        }
        private void GetSab(string text)
        {

            var match = Regex.Match(text, "\"I\":\"(.*?)\",\"LV\":(.?),\"L\":" + $"{SAB}" + ",\"EQH\":(.*?),\"EQC\":(.*?),\"EQT\":(.*?),\"Q\":([0-9]+)");
            if (match.Success)
            {
               
                
                Api._user.Ammo.SAB = double.Parse(match.Groups[6].Value);
                Api._user.Ammo.Ammos.Add(new("SAB", Api._user.Ammo.SAB));

            }
      
        }


    }
}
