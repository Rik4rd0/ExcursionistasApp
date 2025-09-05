using ExcursionistasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExcursionistasApp.Controllers
{
    public class SeleccionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SeleccionParametros parametros, List<Elemento> elementos)
        {
            var resultado = SeleccionarElementosOptimos(parametros, elementos);
            ViewBag.Resultado = resultado;
            ViewBag.Elementos = elementos;
            return View(parametros);
        }

        // Algoritmo tipo mochila (knapsack)
        private SeleccionResultado SeleccionarElementosOptimos(SeleccionParametros parametros, List<Elemento> elementos)
        {
            int n = elementos.Count;
            int pesoMax = parametros.PesoMaximo;
            int minCal = parametros.MinimoCalorias;
            var mejorSeleccion = new List<Elemento>();
            int mejorPeso = int.MaxValue;
            int mejorCalorias = 0;

            // Generar todas las combinaciones posibles
            for (int i = 1; i < (1 << n); i++)
            {
                var seleccion = new List<Elemento>();
                int peso = 0, calorias = 0;
                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        peso += elementos[j].Peso;
                        calorias += elementos[j].Calorias;
                        seleccion.Add(elementos[j]);
                    }
                }
                if (peso <= pesoMax && calorias >= minCal)
                {
                    if (peso < mejorPeso || (peso == mejorPeso && calorias > mejorCalorias))
                    {
                        mejorPeso = peso;
                        mejorCalorias = calorias;
                        mejorSeleccion = new List<Elemento>(seleccion);
                    }
                }
            }

            return new SeleccionResultado
            {
                ElementosSeleccionados = mejorSeleccion,
                CaloriasTotales = mejorCalorias,
                PesoTotal = mejorPeso == int.MaxValue ? 0 : mejorPeso
            };
        }
    }
}
