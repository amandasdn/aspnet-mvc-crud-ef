using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_EF.Models;

namespace CRUD_EF.Controllers
{
    public class UsuariosController : Controller
    {
        private Contexto db = new Contexto();

        bool homologacao = true;
        
        private bool logado()
        {
            if((Session["usuarioLogado"] as Usuario) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            return RedirectToAction("Index");
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            if (logado())
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario u)
        {
            Usuario usuarioLogado = db.Usuarios.ToList().Where(x => x.Email == u.Email && x.Senha == u.Senha).FirstOrDefault();

            if(usuarioLogado != null)
            {
                Session["usuarioLogado"] = usuarioLogado;
                TempData["mensagem"] = "";
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["mensagem"] = "Usuário e/ou senha inválidos.";
            }

            return View(u);
        }

        // GET: Usuarios
        public ActionResult Lista()
        {
            if (!logado())
                return RedirectToAction("Index");
            
            return View(db.Usuarios.ToList());
        }

        public ActionResult Dashboard()
        {
            if (!logado())
                return RedirectToAction("Index");

            return View();
        }

        // GET: Usuarios/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (!logado())
                return RedirectToAction("Index");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Novo
        public ActionResult Novo()
        {
            if (!logado())
                return RedirectToAction("Index");

            return View();
        }

        // POST: Usuarios/Novo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo([Bind(Include = "ID,Nome,Email,Senha,NivelAcesso")] Usuario usuario)
        {
            if (!logado())
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                usuario.DataCadastro = DateTime.Now;
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Lista", new { status = "cadastrado" });
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Editar(int? id)
        {
            if (!logado())
                return RedirectToAction("Index");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "ID,Nome,Email,Senha,DataCadastro,NivelAcesso")] Usuario usuario)
        {
            if (!logado())
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Lista", new { status = "alterado" });
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (!logado())
                return RedirectToAction("Index");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExcluir(int id)
        {
            if (!logado())
                return RedirectToAction("Index");

            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Lista", new { status = "excluido" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
