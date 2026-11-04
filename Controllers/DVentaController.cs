using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ProyectoDesarrollo.B.P.Models;
using ProyectoDesarrollo.B.P.Services;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace ProyectoDesarrollo.B.P.Controllers
{
    public class DVentaController : Controller
    {
        private readonly IDVenta _Dventa;

        public DVentaController(IDVenta Dventa)
        {
            _Dventa = Dventa;
        }
        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                FloatParseHandling = FloatParseHandling.Decimal,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            return settings;
        }
        private string GenerarSiguienteCodigoVenta(string ultimoCodigoVenta)
        {
            string codigoBase = ultimoCodigoVenta.Substring(0, 1); // Obtenemos el primer carácter del código (en este caso, 'V')
            int ultimoNumero = int.Parse(ultimoCodigoVenta.Substring(1)); // Obtenemos el número del último código (por ejemplo, '04')

            int siguienteNumero = ultimoNumero + 1; // Incrementamos el número en 1
            string nuevoNumero = siguienteNumero.ToString("D2"); // Convertimos el número a una cadena de dos dígitos con ceros a la izquierda (por ejemplo, '05')

            return codigoBase + nuevoNumero; // Concatenamos el código base con el nuevo número y retornamos el nuevo código de venta (por ejemplo, 'V05')
        }

        [Obsolete]
        private void GeneratePdf(string htmlContent, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var document = new Document();
                var writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                var htmlWorker = new HTMLWorker(document);
                using (var sr = new StringReader(htmlContent))
                {
                    htmlWorker.Parse(sr);
                }

                document.Close();
            }
        }
        [HttpPost]
        [Obsolete]
        public IActionResult TransferirDetalles(string detalles, string totalVenta)
        {
            string ultimoCodigoVenta = _Dventa.GetUltimoCodigoVenta();
            string nuevoCodigoVenta = GenerarSiguienteCodigoVenta(ultimoCodigoVenta);


            if (!string.IsNullOrEmpty(detalles))
            {

                var detalleArray = JsonConvert.DeserializeObject<TemporalVenta[]>(detalles);
                TbVenta nuevaVenta = new TbVenta();
                nuevaVenta.CodVenta = nuevoCodigoVenta;
                nuevaVenta.CodCliente = "CL01"; // Asignar el código del cliente según corresponda
                nuevaVenta.FecVenta = DateTime.Now; // Asignar la fecha actual o la fecha deseada
                nuevaVenta.TotalVenta = decimal.Parse(totalVenta); // Calcular el total de la venta según tus requisitos
                nuevaVenta.EstadoVenta = "Activo";
                _Dventa.addBb(nuevaVenta);
                if (detalleArray != null && detalleArray.Count() > 0)
                {
                    foreach (var detalle in detalleArray)
                    {
                        TbDetalleVenta tbDetalle = new TbDetalleVenta();
                        tbDetalle.CodVenta = nuevoCodigoVenta;
                        tbDetalle.CodProducto = detalle.codigo;
                        tbDetalle.Precio = (decimal)detalle.precio;
                        tbDetalle.Cantidad = detalle.cantidad;

                        _Dventa.addBa(tbDetalle);
                    }
                    string htmlContent = @"

            <h3>INFORMACIÓN DE LA COMPRA</h3>
            <h3>DATOS CLIENTE</h3>
            <h6>Nombre y Apellidos: Mayerly Damian Gavino</h6>
            <h6>Direción: Av. Javier Prado</h6>
            <h5>Teléfono: 999 989 878</h5>
            <h5>Dirección de correo electrónico: damian@gmail.com</h5>


            <h3>Datos de la compra</h3>
            <table>
                <tr>
                    <th>Código</th>
                    <th>Descripción</th>
                    <th>Precio</th>
                    <th>Cantidad</th>
                    <th>Subtotal</th>
                </tr>";

                    foreach (var clon in detalleArray)
                    {
                        double subtotalItem = clon.precio * clon.cantidad;
                        htmlContent += $@"
                <tr>
                    <td>{clon.codigo}</td>
                    <td>{clon.nombre}</td>
                    <td>{clon.precio}</td>
                    <td>{clon.cantidad}</td>
                    <td>{Math.Round(subtotalItem, 2)}</td>
                </tr>";
                    }

                    double subtotal = detalleArray.Sum(c => c.precio * c.cantidad);
                    double igv = subtotal * 0.18;
                    double total = subtotal + igv;

                    htmlContent += $@"
            </table>
            <div class='subtotal'>
                Subtotal: {Math.Round(subtotal, 2)}
            </div>
            <div class='igv'>
                IGV (18%): {Math.Round(igv, 2)}
            </div>
            <div class='total'>
                Total: {Math.Round(total, 2)}
            </div>";
                    var filePath = "C:\\Users\\dell\\Desktop\\PDF_DBP\\" + nuevoCodigoVenta;
                    filePath += ".pdf";
                    // Generar el PDF
                    GeneratePdf(htmlContent, filePath);
                    return RedirectToAction("Index", "Venta");
                }
            }
            return RedirectToAction("Catalogo", "Producto");
        }
        public IActionResult Boleta()
        {
            return View();
        }
    }
}