using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using Newtonsoft.Json;

using BDFIREBASE.Models;

namespace BDFIREBASE.Controllers
{
    public class MantenedorController : Controller
    {


        IFirebaseClient cliente;


        public MantenedorController() {

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "KjVkACjyp5bNsIH0AwMG73Ex3sXqmBZSggX0iWD9",
                BasePath = "https://bdfirebase-86acd-default-rtdb.firebaseio.com/"

            };

            cliente = new FirebaseClient(config);


        }

        // GET: Mantenedor
        public ActionResult Inicio()
        {
            int? listaFirts = null;

            Dictionary<string, Contacto> lista = new Dictionary<string, Contacto>();
            FirebaseResponse response = cliente.Get("contactos");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                lista = JsonConvert.DeserializeObject<Dictionary<string, Contacto>>(response.Body);

            if(lista != null)
                listaFirts = lista.Count();

            int totalLista = listaFirts ?? 0; 
            

            List<Contacto> listaContacto = new List<Contacto>();
            if ( totalLista != 0) {
                foreach (KeyValuePair<string, Contacto> elemento in lista)
                {

                    listaContacto.Add(new Contacto()
                    {
                        IdContacto = elemento.Key,
                        Nombres = elemento.Value.Nombres,
                        apellidoPaterno = elemento.Value.apellidoPaterno,
                        apellidoMaterno = elemento.Value.apellidoMaterno,
                        Ciudad = elemento.Value.Ciudad,
                        Correo = elemento.Value.Correo,
                        Telefono = elemento.Value.Telefono
                    });

                }
                }

            return View(listaContacto);
        }
        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Editar(string idcontacto)
        {

            FirebaseResponse response = cliente.Get("contactos/" + idcontacto);

            Contacto ocontacto = response.ResultAs<Contacto>();
            ocontacto.IdContacto = idcontacto;

            return View(ocontacto);
        }

        public ActionResult Eliminar(string idcontacto)
        {
            FirebaseResponse response = cliente.Delete("contactos/" + idcontacto);
            return RedirectToAction("Inicio", "Mantenedor");
        }


        [HttpPost]
        public ActionResult Crear(Contacto oContacto)
        {
            string IdGenerado = Guid.NewGuid().ToString("N");

            SetResponse response = cliente.Set("contactos/" + IdGenerado, oContacto);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Inicio", "Mantenedor");
            }
            else {
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult Editar(Contacto oContacto)
        {

            string idcontacto = oContacto.IdContacto;
            oContacto.IdContacto = null;

            FirebaseResponse response = cliente.Update("contactos/" + idcontacto,oContacto);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Inicio", "Mantenedor");
            }
            else
            {
                return View();
            }
        }



    }
}