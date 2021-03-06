﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Business.Constants
{
    public class Messages
    {
        public static string SusccesfulyLogin = "Sisteme Giriş Başarılı !";
        public static string ErrorDatabaseLogin = "Kullanıcı Veritabanında Kayıtlı Değil !";
        public static string ErrorDomainLogin = "Kullanıcı Şifreniz Hatalı, Lütfen Tekrar Deneyiniz !";
       
        public static string ErrorEmteaCode = "Girilen Emtea Kodu ile daha önce Emtea tanımlanmış ! ";
        public static string ErrorDomainUser = "Girilen Kullanıcı ile daha önce tanımlanmış ! ";
        public static string SuccessfulyAddEmtea = "Emtia Veritabanına Eklendi !";
        public static string SuccessfulyAddUser = "Kullanıcı Veritabanına Eklendi !";
        public static string SuccessfulyAddEmteaGruo = "Emtia Grup Veritabanına Eklendi !";
        public static string SuccessfulyAddEmteaType = "Emtia Tipi Veritabanına Eklendi !";
        public static string SuccessfulyAddEmteaTypeGroup = "Emtia Tip Gurubu Veritabanına Eklendi !";
        public static string DataInputControlError = " Bir şehire günde 1 kez kayıt yapılabilir ! ";
        public static string DataInputUpdate = " Güncelleme yapılmıştır ";
        public static string EmteaUpdate = " Emtea Güncellemesi yapılmıştır ";
        public static string UserUpdate = " Kullanıcı Güncellemesi yapılmıştır ";
        public static string EmteaDelete = " Emtea Silme işlemi gerçekleştirilmiştir ";
        public static string TuikDelete = " Tüik Verisi Silme işlemi gerçekleştirilmiştir ";
        public static string EmteaGroupDelete = " Emtea Gurup Silme işlemi gerçekleştirilmiştir ";
        public static string UserDelete = " Kullanıcı Silme işlemi gerçekleştirilmiştir ";
        public static string EmteaTypeGroupDelete = " Emtea Tip Gurup Silme işlemi gerçekleştirilmiştir ";
        public static string EmteaTypeDelete = " Emtea Tip Silme işlemi gerçekleştirilmiştir ";
        public static string EmteaDeleteError = " Emtea Silme işlemi sırasında bir hata oluştu ! ";
        public static string DataInputAdd = "Veri girişleriniz veritabanına eklendi !";
        public static string EmteaTypeGroupUpdated = "Emtea Tip çeşidi güncellendi !";
        public static string IsLockSuccess = "Şube - İl için veri giriş ayarlaması yapıldı !";
        public static string IsLockFailt = "Veri kilitleme başarısız !";
        public static string TuikSubeAdd = "Tuik Şube verisi veritabanına  kaydedildi !";
        public static string TuikCityAdd = "Tuik İl verisi veritabanına  kaydedildi !";
        public static string TuikCityUpdate = "Tuik verisi güncellendi !";
        public static string TuikSubeAddError = "Tuik Şube verisi veritabanına  kaydedilemedi !";

        public static string ErrorEmteaGroupName = "Girilen Emtea Grup Adı ile daha önce Emtea  Grup tanımlanmış ! ";
        public static string ErrorEmteaTypeCode = "Girilen Emtia Tip Kodu ile daha önce Emtia Tipi tanımlanmış ! ";
        public static string ErrorEmteaTypeGroupName = "Girilen Emtea Tip Grup Adı ile daha önce Emtea  Grup tanımlanmış ! ";
        public static string ErrorEmteaTypeName = "Girilen Emtea Tip Kodu ile daha önce Emtea Tipi tanımlanmış ! ";
        public static string ErrorAddTuikSubeData = "Seçilen işyerine amevcut yıl içerisinde seçilem emtia tipi ile 1 kez kayıt yapılabilir ! ";
        public static string ErrorAddTuikCityData = "Seçilen şehire amevcut yıl içerisinde seçilem emtia tipi ile 1 kez kayıt yapılabilir ! ";
        public static string ErrorAdd = "Veritabanına kayıt edilemedi ! ";
       
    }
}
