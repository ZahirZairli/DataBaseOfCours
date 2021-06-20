using KursLa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KursLa.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Employer()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id); 

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            if (tip == 1)
            {
            List<bildiris> maases= db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();
            List<IsciG> isci = new List<IsciG>();
            ViewBag.isci = db.IsciG.ToList();
            List<Vezife> vezife = db.Vezife.ToList();
            ViewBag.vezife = db.Vezife.ToList();
            List<Filial> filial= db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<IsciG> isci = new List<IsciG>();
                ViewBag.isci = db.IsciG.Where(x => x.IsciFilialId == filialid).ToList();

                List<Vezife> vezife = db.Vezife.ToList();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();
            }
            return View();
        }
        public ActionResult yeniisci(Isci yeniisci)
        {
            db.Isci.InsertOnSubmit(yeniisci);
            db.SubmitChanges();
           return RedirectToAction("Employer");
        }
        public ActionResult yenivezife(Vezife yenivezife)
        {
            db.Vezife.InsertOnSubmit(yenivezife);
            db.SubmitChanges();
            return RedirectToAction("jobfilial");
        }
        public ActionResult yenifilial(Filial yenifilial)
        {
            db.Filial.InsertOnSubmit(yenifilial);
            db.SubmitChanges();
            return RedirectToAction("jobfilial");
        }
        public ActionResult silisci(int id)
        {
            db.Isci.DeleteOnSubmit(db.Isci.SingleOrDefault(x => x.IsciId == id));
            db.SubmitChanges();
            return RedirectToAction("Employer");
        }
        public ActionResult silvezife(int id)
        {
            db.Vezife.DeleteOnSubmit(db.Vezife.SingleOrDefault(x => x.VezifeId == id));
            db.SubmitChanges();
            return RedirectToAction("jobfilial");
        }
        public ActionResult silfilial(int id)
        {
            db.Filial.DeleteOnSubmit(db.Filial.SingleOrDefault(x => x.FilialId == id));
            db.SubmitChanges();
            return RedirectToAction("jobfilial");
        }
        public ActionResult duzelisisci(int id, Isci yeniisci)
        {
            Isci oldisci = db.Isci.SingleOrDefault(x => x.IsciId == id);
            oldisci.IsciFilialId = yeniisci.IsciFilialId;
            oldisci.IsciMaas = yeniisci.IsciMaas;
            oldisci.IsciMaasGunu = yeniisci.IsciMaasGunu;
            oldisci.IsciMuqavileBaqlanmaTarixi = yeniisci.IsciMuqavileBaqlanmaTarixi;
            oldisci.IsciMuqavileBitmeTarixi = yeniisci.IsciMuqavileBitmeTarixi;
            oldisci.IsciTamAd = yeniisci.IsciTamAd;
            oldisci.IsciTelefon = yeniisci.IsciTelefon;
            oldisci.IsciVezifeId = yeniisci.IsciVezifeId;
            oldisci.IsciMaasFaiz = yeniisci.IsciMaasFaiz;
            db.SubmitChanges();
            return RedirectToAction("Employer");
        }
        public ActionResult editisci(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3 = cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            if (tip == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();
                List<IsciG> isci = new List<IsciG>();
                ViewBag.isci = db.IsciG.ToList();
                List<Vezife> vezife = db.Vezife.ToList();
                ViewBag.vezife = db.Vezife.ToList();
                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.ToList();
                var editisci = db.Isci.SingleOrDefault(x => x.IsciId == id);
                return View(editisci);
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();
                
                List<IsciG> isci = new List<IsciG>();
                ViewBag.isci = db.IsciG.Where(x => x.FilialId == filialid).ToList();

                List<Vezife> vezife = db.Vezife.ToList();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                var editisci = db.Isci.SingleOrDefault(x => x.IsciId == id);
                return View(editisci);
            }

        }
        public ActionResult editvezife(int id)
        {
            List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();
            var editvezife = db.Vezife.SingleOrDefault(x => x.VezifeId == id);
            return View(editvezife);
        }
        public ActionResult editfilial(int id)
        {
            List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();
            var editfilial = db.Filial.SingleOrDefault(x => x.FilialId == id);
            return View(editfilial);
        }
        public ActionResult upvezife(int id, Vezife yenivezife)
        {
            Vezife oldvezife= db.Vezife.SingleOrDefault(x => x.VezifeId == id);
            oldvezife.VezifeAd = yenivezife.VezifeAd;
            db.SubmitChanges();
            return RedirectToAction("jobfilial");
        }
        public ActionResult upfilial(int id, Filial yenifilial)
        {
            Filial oldfilial= db.Filial.SingleOrDefault(x => x.FilialId == id);
            oldfilial.FilialAd = yenifilial.FilialAd;
            db.SubmitChanges();
            return RedirectToAction("jobfilial");
        }
        public ActionResult jobfilial()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            if (tip == 1)
            {
            List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();
            List<Vezife> vezife = new List<Vezife>();
            ViewBag.vezife = db.Vezife.ToList();
            List<Filial> filial = db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();
            return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }


        }
        public ActionResult Telebeler()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            if (tip == 1)
            {
                List<Isci> isci = new List<Isci>();
                ViewBag.isci = db.Isci.ToList();

                List<DersPaket> paket = new List<DersPaket>();
                ViewBag.paket = db.DersPaket.ToList();


                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.ToList();

                List<Vezife> vezife = new List<Vezife>();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.ToList();
            }
            else
            {
                List<Isci> isci = new List<Isci>();
                ViewBag.isci = db.Isci.Where(x => x.IsciFilialId == filialid).ToList();

                List<DersPaket> paket = new List<DersPaket>();
                ViewBag.paket = db.DersPaket.Where(x => x.PaketFilialId == filialid).ToList();


                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.Where(x => x.TelebeFilialId == filialid).ToList();

                List<Vezife> vezife = new List<Vezife>();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();
            }
            return View();
        }

        public ActionResult yenitelebe(telebe yenitelebe)
        {
            var id = Convert.ToInt32(yenitelebe.TelebePaketId);
            var idis = Convert.ToInt32(yenitelebe.TelebeIsciId);
            var tarix = yenitelebe.TelebeQeydiyyatTarixi.Year;
            if (tarix == 1)
            {
                yenitelebe.TelebeQeydiyyatTarixi = DateTime.Now;
            }

            Isci oldisci = db.Isci.SingleOrDefault(x => x.IsciId == idis);
            var vz = oldisci.IsciVezifeId;
            var vz2 = db.Vezife.SingleOrDefault(x => x.VezifeId == vz);
            string vezife = vz2.VezifeAd;
            var zahir = db.DersPaket.SingleOrDefault(x => x.PaketId == id);
             var ayliq = zahir.PaketQiymetAyliq;
            var endirim = yenitelebe.TelebeEndirim;
            var faiz = ayliq - endirim;
            var kohnefaiz = oldisci.IsciMaasFaiz;
            if (vezife.ToLower().Contains("mentor"))
            {
            var toplamfaiz = kohnefaiz + (faiz*20)/100;
            oldisci.IsciMaasFaiz = (double)toplamfaiz;
            }
            else
            {
                var toplamfaiz = kohnefaiz + (faiz * 50) / 100;
            oldisci.IsciMaasFaiz = (double)toplamfaiz;
            }
            db.telebe.InsertOnSubmit(yenitelebe);
            db.SubmitChanges();
            return RedirectToAction("Telebeler");
        }
        public ActionResult siltelebe(int id)
        {
            telebe old = db.telebe.SingleOrDefault(x => x.TelebeId == id);
            var id2 = Convert.ToInt32(old.TelebePaketId);
            var idis = Convert.ToInt32(old.TelebeIsciId);
            Isci oldisci = db.Isci.SingleOrDefault(x => x.IsciId == idis);
            
            var vz = oldisci.IsciVezifeId;
            var vz2 = db.Vezife.SingleOrDefault(x => x.VezifeId == vz);
            string vezife = vz2.VezifeAd;
            var zahir = db.DersPaket.SingleOrDefault(x => x.PaketId == id2);
            if (zahir != null)
            {
            var ayliq = zahir.PaketQiymetAyliq;
            var endirim = old.TelebeEndirim;
            var faiz = ayliq - endirim;
            var kohnefaiz = oldisci.IsciMaasFaiz;
            if (vezife.ToLower().Contains("mentor"))
            {
                var toplamfaiz = kohnefaiz - (faiz * 20) / 100;
                oldisci.IsciMaasFaiz = (double)toplamfaiz;
            }
            else
            {
                var toplamfaiz = kohnefaiz - (faiz * 50) / 100;
                oldisci.IsciMaasFaiz = (double)toplamfaiz;
            }
            }
            db.telebe.DeleteOnSubmit(db.telebe.SingleOrDefault(x => x.TelebeId == id));
            db.SubmitChanges();
            return RedirectToAction("Telebeler");
        }

        public ActionResult edittelebe(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3= cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            if (tip == 1)
            {
                List<Isci> isci = new List<Isci>();
                ViewBag.isci = db.Isci.ToList();

                List<DersPaket> paket = new List<DersPaket>();
                ViewBag.paket = db.DersPaket.ToList();


                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.ToList();

                List<Vezife> vezife = new List<Vezife>();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.ToList();

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                var edittelebe = db.telebe.SingleOrDefault(x => x.TelebeId == id);
                return View(edittelebe);
            }
            else
            {
                List<Isci> isci = new List<Isci>();
                ViewBag.isci = db.Isci.Where(x => x.IsciFilialId == filialid).ToList();

                List<DersPaket> paket = new List<DersPaket>();
                ViewBag.paket = db.DersPaket.Where(x => x.PaketFilialId == filialid).ToList();


                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.Where(x => x.FilialId == filialid).ToList();

                List<Vezife> vezife = new List<Vezife>();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                var edittelebe = db.telebe.SingleOrDefault(x => x.TelebeId == id);
                return View(edittelebe);
            }
        }

        public ActionResult uptelebe(int id, telebe yenitelebe)
        {
            telebe oldtelebe= db.telebe.SingleOrDefault(x => x.TelebeId == id);
            oldtelebe.TelebeTamAd = yenitelebe.TelebeTamAd;
            oldtelebe.TelebeQeydiyyatTarixi = yenitelebe.TelebeQeydiyyatTarixi;
            oldtelebe.TelebePaketId = yenitelebe.TelebePaketId;
            oldtelebe.TelebeOdenisGunu = yenitelebe.TelebeOdenisGunu;
            oldtelebe.TelebeFilialId = yenitelebe.TelebeFilialId;
            oldtelebe.TelebeEndirim = yenitelebe.TelebeEndirim;
            oldtelebe.TelebeIsciId = yenitelebe.TelebeIsciId;
            db.SubmitChanges();
            return RedirectToAction("Telebeler");
        }

        public ActionResult paketlər()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
            List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();

            List<Filial> filial= db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();

            List<paketmain> paket = db.paketmain.ToList();
            ViewBag.paket = db.paketmain.ToList();

            return View();
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<paketmain> paket = db.paketmain.ToList();
                ViewBag.paket = db.paketmain.Where(x => x.PaketFilialId == filialid).ToList();
                return View();
            }


        }

        public ActionResult yenipaket(DersPaket yenipaket)
        {
            db.DersPaket.InsertOnSubmit(yenipaket);
            db.SubmitChanges();
            return RedirectToAction("paketlər");
        }

        public ActionResult silpaket(int id)
        {
            db.DersPaket.DeleteOnSubmit(db.DersPaket.SingleOrDefault(x => x.PaketId == id));
            db.SubmitChanges();
            return RedirectToAction("paketlər");
        }

        public ActionResult editpaket(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3 = cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            { 
            List<Isci> isci = new List<Isci>();
            ViewBag.isci = db.Isci.ToList();

            List<DersPaket> paket = new List<DersPaket>();
            ViewBag.paket = db.DersPaket.ToList();

            List<telebemain> telebe = db.telebemain.ToList();
            ViewBag.telebe = db.telebemain.ToList();

            List<Vezife> vezife = new List<Vezife>();
            ViewBag.vezife = db.Vezife.ToList();

            List<Filial> filial = db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();

            List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();

            var editpaket = db.DersPaket.SingleOrDefault(x => x.PaketId == id);
            return View(editpaket);
            }
            else
            {
                List<Isci> isci = new List<Isci>();
                ViewBag.isci = db.Isci.Where(x => x.IsciFilialId == filialid).ToList();

                List<DersPaket> paket = new List<DersPaket>();
                ViewBag.paket = db.DersPaket.Where(x => x.PaketFilialId == filialid).ToList();

                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.Where(x => x.TelebeFilialId == filialid).ToList();

                List<Vezife> vezife = new List<Vezife>();
                ViewBag.vezife = db.Vezife.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                var editpaket = db.DersPaket.SingleOrDefault(x => x.PaketId == id);
                return View(editpaket);
            }
        }

        public ActionResult uppaket(int id, DersPaket yenipaket)
        {
            DersPaket oldpaket= db.DersPaket.SingleOrDefault(x => x.PaketId == id);
            oldpaket.PaketAd = yenipaket.PaketAd;
            oldpaket.PaketFilialId = yenipaket.PaketFilialId;
            oldpaket.PaketMuddetiAy = yenipaket.PaketMuddetiAy;
            oldpaket.PaketQiymetAyliq = yenipaket.PaketQiymetAyliq;
            db.SubmitChanges();
            return RedirectToAction("paketlər");
        }


        public ActionResult xercler()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();

            List<Filial> filial = db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();

            List<xercfil> xerc = db.xercfil.ToList();
            ViewBag.xerc = db.xercfil.ToList();

            return View();

            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<xercfil> xerc = db.xercfil.ToList();
                ViewBag.xerc = db.xercfil.Where(x => x.FilialId == filialid).ToList();

                return View();
            }




        }
        public ActionResult editxerc(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3 = cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.ToList();

                List<xercfil> xerc = db.xercfil.ToList();
                ViewBag.xerc = db.xercfil.ToList();

                var editxerc = db.Xercler.SingleOrDefault(x => x.XercId == id);
                return View(editxerc);
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<xercfil> xerc = db.xercfil.ToList();
                ViewBag.xerc = db.xercfil.Where(x => x.XercFilialId == filialid).ToList();

                var editxerc = db.Xercler.SingleOrDefault(x => x.XercId == id);
                return View(editxerc);
            }
        }

        public ActionResult upxerc(int id, Xercler yenixerc)
        {
            Xercler oldxerc= db.Xercler.SingleOrDefault(x => x.XercId == id);
            oldxerc.XercAd = yenixerc.XercAd;
            oldxerc.XercFilialId = yenixerc.XercFilialId;
            oldxerc.XercMiqdar = yenixerc.XercMiqdar;
            oldxerc.XercTarix = yenixerc.XercTarix;
            db.SubmitChanges();
            return RedirectToAction("xercler");
        }

        public ActionResult yenixerc(Xercler yenixerc)
        {
            if (yenixerc.XercTarix == null)
            {
            yenixerc.XercTarix = DateTime.Now;
            }
            db.Xercler.InsertOnSubmit(yenixerc);
            db.SubmitChanges();
            return RedirectToAction("xercler");
        }

        public ActionResult silxerc(int id)
        {
            db.Xercler.DeleteOnSubmit(db.Xercler.SingleOrDefault(x => x.XercId == id));
            db.SubmitChanges();
            return RedirectToAction("xercler");
        }

        public ActionResult bilidiris()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();

            List<bildiris3> bil3 = db.bildiris3.ToList();
            ViewBag.bil3 = db.bildiris3.ToList();
            return View();

            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<bildiris3> bil3 = db.bildiris3.ToList();
                ViewBag.bil3 = db.bildiris3.Where(x => x.FilialId == filialid).ToList();
                return View();

            }

        }
        public ActionResult arenda()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();

            List<Filial> filial = db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();

            List<arendamain> armain = new List<arendamain>();
            ViewBag.armain = db.arendamain.ToList();

            List<arenda> arenda = new List<arenda>();
            ViewBag.arenda = db.arenda.ToList();
            return View();

            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<arendamain> armain = new List<arendamain>();
                ViewBag.armain = db.arendamain.Where(x => x.ArendaFilialId == filialid).ToList();

                List<arenda> arenda = new List<arenda>();
                ViewBag.arenda = db.arenda.Where(x => x.ArendaFilialId == filialid).ToList();
                return View();
            }



        }

        public ActionResult yeniarenda(arenda yeniarenda)
        {
            yeniarenda.ArendaTarix = DateTime.Now;
            db.arenda.InsertOnSubmit(yeniarenda);
            db.SubmitChanges();
            return RedirectToAction("arenda");
        }
        public ActionResult silarenda(int id)
        {
            db.arenda.DeleteOnSubmit(db.arenda.SingleOrDefault(x => x.ArendaId== id));
            db.SubmitChanges();
            return RedirectToAction("arenda");
        }

        public ActionResult editarenda(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3 = cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.ToList();

                List<arendamain> armain = new List<arendamain>();
                ViewBag.armain = db.arendamain.ToList();

                List<arenda> arenda = new List<arenda>();
                ViewBag.arenda = db.arenda.ToList();

                var editarenda = db.arenda.SingleOrDefault(x => x.ArendaId == id);
                return View(editarenda);
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();


                List<Filial> filial = db.Filial.ToList();
                ViewBag.filial = db.Filial.Where(x => x.FilialId == filialid).ToList();

                List<arendamain> armain = new List<arendamain>();
                ViewBag.armain = db.arendamain.Where(x => x.ArendaFilialId == filialid).ToList();

                List<arenda> arenda = new List<arenda>();
                ViewBag.arenda = db.arenda.Where(x => x.ArendaFilialId == filialid).ToList();

                var editarenda = db.arenda.SingleOrDefault(x => x.ArendaId == id);
                return View(editarenda);
            }
        }

        public ActionResult uparenda(int id, arenda yeniarenda)
        {
            arenda oldarenda = db.arenda.SingleOrDefault(x => x.ArendaId == id);
            oldarenda.ArendaMiqdari = yeniarenda.ArendaMiqdari;
            oldarenda.ArendaGunu = yeniarenda.ArendaGunu;
            oldarenda.ArendaFilialId = yeniarenda.ArendaFilialId;
            db.SubmitChanges();
            return RedirectToAction("arenda");
        }






        //----------------------------SignIn-----------------------------

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult SignIn(string IstifadeciMail, string IstifadeciParol)
        {
            var c = CreateMD5("levengi" + IstifadeciParol + "levengi");
            DataTable dt = Sql.Exec($"select * from Istifadeci where IstifadeciMail=N'{IstifadeciMail}' and IstifadeciParol=N'{c}'");
            if (dt.Rows.Count == 0 || IstifadeciMail == null || IstifadeciParol == null)
            {
                TempData["Message"] = "Istifadəçi mailiniz və ya parolunuz yanlışdır.Zəhmət olmasa yenidən cəhd edin!";
                return RedirectToAction("AdminLogin");
            }
            else
            {
                TempData["Message"] = "Əməliyyat uğurla həyata keçirildi!";
                HttpCookie cookie = new HttpCookie("User");
                cookie.Expires = DateTime.Now.AddDays(1d);
                cookie.Values.Add("UserId", dt.Rows[0][0].ToString());
                cookie.Values.Add("UserName", dt.Rows[0][1].ToString());
                cookie.Values.Add("UserTip", dt.Rows[0][5].ToString());
                Response.Cookies.Add(cookie);
                return RedirectToAction("Telebeler");
            }
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("User");
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);
            return RedirectToAction("AdminLogin");
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        public ActionResult kodal(string IstifadeciMail)
        {
            DataTable dt = Sql.Exec($"Select * from Istifadeci where IstifadeciMail=N'{IstifadeciMail}'");
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0][6].ToString() == IstifadeciMail)
                {
                    Random a = new Random();
                    var kod = a.Next(100000, 9999999);
                    MailMessage mesajim = new MailMessage();
                    SmtpClient istemci = new SmtpClient();
                    istemci.Credentials = new System.Net.NetworkCredential("zairli.zahir@hotmail.com", "tankionline2017");
                    istemci.Port = 587;
                    istemci.Host = "smtp.live.com";
                    istemci.EnableSsl = true;
                    mesajim.To.Add(IstifadeciMail);
                    mesajim.From = new MailAddress("zairli.zahir@hotmail.com");
                    mesajim.Subject = "The Code for RePassword";
                    mesajim.Body = kod.ToString();
                    istemci.Send(mesajim);
                    TempData["Message"] = "Əməliyyat uğurla həyata keçirildi!";
                    Sql.Exec($"Update  Istifadeci set IstifadeciQurtar=N'{kod}' where IstifadeciMail=N'{IstifadeciMail }'");
                    return RedirectToAction("UpdatePassword");
                }
                else
                {
                    TempData["Message"] = "Bu maildə istifadəçi tapılmadı";
                    return RedirectToAction("ForgetPass");
                }
            }
            else
            {
                TempData["Message"] = "Bu maildə istifadəçi tapılmadı!";
                return RedirectToAction("ForgetPass");
            }

        }


        public ActionResult UpdatePassword()
        {
            return View();
        }

        public ActionResult UpdatePass(string IstifadeciQurtar, string IstifadeciParol, string IstifadeciMail)
        {
            DataTable dt = Sql.Exec($"Select * from Istifadeci where IstifadeciMail=N'{IstifadeciMail}' and IstifadeciQurtar=N'{IstifadeciQurtar}' ");
            if (dt.Rows.Count != 0)
            {
                var l = CreateMD5("levengi" + IstifadeciParol + "levengi");
                if (IstifadeciQurtar == dt.Rows[0][8].ToString() && IstifadeciQurtar != null && IstifadeciQurtar != "0")
                {
                    Sql.Exec($"Update Istifadeci set IstifadeciParol=N'{l}', IstifadeciQurtar='0' where IstifadeciMail=N'{IstifadeciMail}'");
                    TempData["Message"] = "Kodunuz uğurla dəyişdirildi!";
                    return RedirectToAction("AdminLogin");
                }
                else
                {
                    TempData["Message"] = "Mail və ya random kod yanlışdır!";
                    return RedirectToAction("ForgetPass");
                }
            }
            else
            {
                TempData["Message"] = "Mail və ya random kod yanlışdır!";
                return RedirectToAction("ForgetPass");
            }
        }



        public ActionResult Istifadeciler() 
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<istifadecimain> istifadeci = new List<istifadecimain>();
            ViewBag.istifadeci = db.istifadecimain.ToList();

            List<Filial> filial = db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();

            List<istip> tip = db.istip.ToList();
            ViewBag.tip = db.istip.ToList();

            return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        public ActionResult yeniistifadeci(Istifadeci newis, string IstifadeciParol1)
        {
            var l = CreateMD5("levengi" + IstifadeciParol1 + "levengi");

            newis.IstifadeciParol = l;
            newis.IstifadeciPhoto = "Photo20216110742319.jpg";
            db.Istifadeci.InsertOnSubmit(newis);
            db.SubmitChanges();
            return RedirectToAction("Istifadeciler");
        }


        public ActionResult silistifadeci(int id)
        {
            db.Istifadeci.DeleteOnSubmit(db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id));
            db.SubmitChanges();
            return RedirectToAction("Istifadeciler");
        }


        public ActionResult editistifadeci(int id)
        {
            List<bildiris> maases = db.bildiris.ToList();
            ViewBag.maases = db.bildiris.ToList();

            
            List<Filial> filial = db.Filial.ToList();
            ViewBag.filial = db.Filial.ToList();

            List<istip> tip = db.istip.ToList();
            ViewBag.tip = db.istip.ToList();


            var istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id);
            return View(istifadeci);
        }

        public ActionResult upistifadeci(int id, Istifadeci yeniis)
        {
            Istifadeci oldis= db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id);
            oldis.IstifadeciFilialId = yeniis.IstifadeciFilialId;
            oldis.IstifadeciMail = yeniis.IstifadeciMail;
            oldis.IstifadeciTamAd = yeniis.IstifadeciTamAd;
            oldis.IstifadeciTelefon = yeniis.IstifadeciTelefon;
            oldis.IstifadeciTipId = yeniis.IstifadeciTipId;
            var l = CreateMD5("levengi" + yeniis.IstifadeciParol + "levengi");
            oldis.IstifadeciParol = l;
            db.SubmitChanges();
            return RedirectToAction("Istifadeciler");
        }

        public ActionResult user()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            var pr = db.istifadecimain.SingleOrDefault(x => x.IstifadeciId == id2);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();
            }

                return View(pr);
        }

        public ActionResult upus(int id, HttpPostedFileBase IstifadeciPhoto)
        {
            if (IstifadeciPhoto != null)
            {
            string PhotoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(IstifadeciPhoto.FileName);
            IstifadeciPhoto.SaveAs(Server.MapPath("~/assets/img/faces/" + PhotoName));
            Istifadeci oldis = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id);
            oldis.IstifadeciPhoto = PhotoName;
            db.SubmitChanges();
            }
            return RedirectToAction("user");
        }






        //-------------------------------------------------------TelebeOdenisi----------------------------------------------
        public ActionResult TelebeOdenisi()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            List<tarixtelebemain> tarix = new List<tarixtelebemain>();
            ViewBag.tarix = db.tarixtelebemain.OrderBy(x => x.Year).ToList();

            if (tip == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<telebemain> telebe = db.telebemain.OrderBy(x => x.TelebeTamAd).ToList();
                ViewBag.telebe = db.telebemain.ToList();

                List<telebeodenismain> telebeodenismain = db.telebeodenismain.ToList();
                ViewBag.telebeodenismain = db.telebeodenismain.ToList();
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.Where(x => x.TelebeFilialId == filialid).OrderBy(x => x.TelebeTamAd).ToList();


                List<telebeodenismain> telebeodenismain = db.telebeodenismain.ToList();
                ViewBag.telebeodenismain = db.telebeodenismain.Where(x => x.TelebeFilialId == filialid).ToList();

            }
            return View();
        }

        public ActionResult yenitelebeodenis(telebeodenis yenitelebeodenis, string TelebeodenisOdediyiGunu)
        {
            if (TelebeodenisOdediyiGunu == "")
            {
                yenitelebeodenis.TelebeodenisOdediyiGunu = DateTime.Now;
            }
            db.telebeodenis.InsertOnSubmit(yenitelebeodenis);
            db.SubmitChanges();
            return RedirectToAction("TelebeOdenisi");
        }

        public ActionResult siltelebeodenis(int id)
        {
            db.telebeodenis.DeleteOnSubmit(db.telebeodenis.SingleOrDefault(x => x.TelebeodenisId == id));
            db.SubmitChanges();
            return RedirectToAction("TelebeOdenisi");
        }



        public ActionResult edittelebeodenis(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3 = cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.OrderBy(x => x.TelebeTamAd).ToList();

                var telebeodenis = db.telebeodenis.SingleOrDefault(x => x.TelebeodenisId == id);
                return View(telebeodenis);
            }
            else
            {
                
                List<telebemain> telebe = db.telebemain.ToList();
                ViewBag.telebe = db.telebemain.Where(x => x.TelebeFilialId == filialid).OrderBy(x => x.TelebeTamAd).ToList();

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                var telebeodenis = db.telebeodenis.SingleOrDefault(x => x.TelebeodenisId == id);
                return View(telebeodenis);
            }
        }

        public ActionResult uptelebeodenis(int id, telebeodenis yeniodenis)
        {
            telebeodenis oldodenis= db.telebeodenis.SingleOrDefault(x => x.TelebeodenisId == id);
            oldodenis.TelebeodenisOdediyiGunu = yeniodenis.TelebeodenisOdediyiGunu;
            oldodenis.TelebeodenisOdediyiMebleq = yeniodenis.TelebeodenisOdediyiMebleq;
            oldodenis.TelebeodenisTelebeId = yeniodenis.TelebeodenisTelebeId;
            db.SubmitChanges();
            return RedirectToAction("TelebeOdenisi");
        }






        public ActionResult IsciOdenisi()
        {
            var cookie = Request.Cookies["User"];
            var id = cookie["UserId"];
            var id2 = Convert.ToInt32(id);

            List<Istifadeci> istifadeci = new List<Istifadeci>();
            ViewBag.istifadeci = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci.IstifadeciFilialId;
            var tip = ViewBag.istifadeci.IstifadeciTipId;

            List<tarixiscimain> tarix = new List<tarixiscimain>();
            ViewBag.tarix = db.tarixiscimain.OrderBy(x => x.Year).ToList();


            if (tip == 1)
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<IsciG> isci = db.IsciG.OrderBy(x => x.IsciTamAd).ToList();
                ViewBag.isci = db.IsciG.ToList();

                List<isciodenismain> isciodenismain = db.isciodenismain.ToList();
                ViewBag.isciodenismain = db.isciodenismain.ToList();
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();
                List<IsciG> telebe = db.IsciG.ToList();
                ViewBag.isci = db.IsciG.Where(x => x.IsciFilialId == filialid).OrderBy(x => x.IsciTamAd).ToList();
                List<isciodenismain> isciodenismain = db.isciodenismain.ToList();
                ViewBag.isciodenismain = db.isciodenismain.Where(x => x.IsciFilialId == filialid).ToList();
            }
            return View();
        }


        public ActionResult yeniisciodenis(isciodenis yeniodenis, string IsciodenisOdenildiyiGun)
        {
            if (IsciodenisOdenildiyiGun == "")
            {
                yeniodenis.IsciodenisOdenildiyiGun = DateTime.Now;
            }
            db.isciodenis.InsertOnSubmit(yeniodenis);
            db.SubmitChanges();
            return RedirectToAction("IsciOdenisi");
        }

        public ActionResult silisciodenis(int id)
        {
            db.isciodenis.DeleteOnSubmit(db.isciodenis.SingleOrDefault(x => x.IsciodenisId == id));
            db.SubmitChanges();
            return RedirectToAction("IsciOdenisi");
        }








        public ActionResult editisciodenis(int id)
        {
            var cookie = Request.Cookies["User"];
            var id3 = cookie["UserId"];
            var id2 = Convert.ToInt32(id3);

            List<Istifadeci> istifadeci1 = new List<Istifadeci>();
            ViewBag.istifadeci1 = db.Istifadeci.SingleOrDefault(x => x.IstifadeciId == id2);

            int filialid = ViewBag.istifadeci1.IstifadeciFilialId;
            var tip2 = ViewBag.istifadeci1.IstifadeciTipId;

            if (tip2 == 1)
            {

                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.ToList();

                List<IsciG> isci= db.IsciG.ToList();
                ViewBag.isci = db.IsciG.OrderBy(x => x.IsciTamAd).ToList();

                var isciodenis = db.isciodenis.SingleOrDefault(x => x.IsciodenisId == id);
                return View(isciodenis);
            }
            else
            {
                List<bildiris> maases = db.bildiris.ToList();
                ViewBag.maases = db.bildiris.Where(x => x.FilialId == filialid).ToList();

                List<IsciG> isci = db.IsciG.ToList();
                ViewBag.isci = db.IsciG.Where(x => x.IsciFilialId == filialid).OrderBy(x => x.IsciTamAd).ToList();

                var isciodenis = db.isciodenis.SingleOrDefault(x => x.IsciodenisId == id);
                return View(isciodenis);
            }
        }


        public ActionResult upisciodenis(int id, isciodenis yeniodenis)
        {
            isciodenis oldodenis = db.isciodenis.SingleOrDefault(x => x.IsciodenisId == id);
            oldodenis.IsciodenisIsciId = yeniodenis.IsciodenisIsciId;
            oldodenis.IsciodenisOdenildiyiGun = yeniodenis.IsciodenisOdenildiyiGun;
            oldodenis.IsciodenisOdenilenMebleq = yeniodenis.IsciodenisOdenilenMebleq;   
            db.SubmitChanges();
            return RedirectToAction("IsciOdenisi");
        }


    }
}