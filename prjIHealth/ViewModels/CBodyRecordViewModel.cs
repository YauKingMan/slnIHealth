﻿using prjiHealth.Models;
using prjIHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjiHealth.ViewModels
{
    public class CBodyRecordViewModel
    {
        IHealthContext db = new IHealthContext();
        public int FBodyRecordId { get; set; }
        public int? FMemberId { get; set; }
        public string FRecordDate { get; set; }
        public double? FWorkload { get; set; }
        public double? FHeight { get; set; }
        public double? FWeight { get; set; }
        public virtual TMember FMember { get; set; }
        public int? Age 
        {
            get 
            {
                var theMember = db.TMembers.FirstOrDefault(m => m.FMemberId == FMemberId);
                if (theMember != null)
                {
                    DateTime bday = DateTime.Parse(theMember.FBirthday);
                    return (bday > DateTime.Today.AddYears(-(DateTime.Today.Year - bday.Year))) ? DateTime.Today.Year - bday.Year - 1 : DateTime.Today.Year - bday.Year;
                }
                else
                    return Age;
            }
            set 
            {
                Age = value;
            } 
        }
        public int? Gender 
        { 
            get 
            {
                var theMember = db.TMembers.FirstOrDefault(m => m.FMemberId == FMemberId);
                if (theMember != null)
                {
                    return ((bool)(theMember.FGender))?1:0;
                }
                else
                    return Gender;
            }
            set
            {
                Gender = value;
            } 
        }
        public double NumBMI
        {
            get
            {
                return Math.Round((double)FWeight / Math.Pow((double)(FHeight / 100), 2),1);
            }
        }
        public double NumFat
        {
            get
            {
                double numBMI = (double)FWeight / Math.Pow((double)(FHeight / 100), 2);
                return Math.Round(1.2 * numBMI + (0.23 * (double)Age) - 5.4 - (10.8 * (double)Gender), 2);
            }
        }
        public double NumTDEE
        {
            get
            {
                if (Gender == 1)
                {
                    return Math.Round((((10 * (double)FWeight) + (6.25 * (double)FHeight) - (5 * (double)Age) +  5 ) * (double)FWorkload),0);
                }
                else
                {
                    return Math.Round((((10 * (double)FWeight) + (6.25 * (double)FHeight) - (5 * (double)Age) - 161) * (double)FWorkload),0);
                }
                
            }
        }
    }
}