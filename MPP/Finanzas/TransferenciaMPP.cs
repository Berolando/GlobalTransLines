using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE.Finanzas;
using MPP.Transversal;

namespace MPP.Finanzas
{
    public class TransferenciaMPP : IRepositorio<BETransferencia>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BETransferencia>();

        #region "Alta"

        public void Alta(BETransferencia obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;

                var nuevaTransferencia = new XElement("Transferencia",
                    new XAttribute("Id", obj.Id),
                    new XElement("Banco", obj.Banco),
                    new XElement("CBU", obj.CBU)
                );

                var root = doc.Element("Transferencias");
                if (root == null)
                    doc.Add(new XElement("Transferencias", nuevaTransferencia));
                else
                    root.Add(nuevaTransferencia);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de transferencias", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de transferencias. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar transferencia en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BETransferencia obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var nodo = doc.Descendants("Transferencia")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de transferencias", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de transferencias. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar transferencia en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BETransferencia obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var nodo = doc.Descendants("Transferencia")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.SetElementValue("Banco", obj.Banco);
                    nodo.SetElementValue("CBU", obj.CBU);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de transferencias", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de transferencias. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar transferencia en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BETransferencia> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Transferencia")
                          .Select(x => new BETransferencia
                          {
                              Id = (long?)x.Attribute("Id") ?? 0,
                              Banco = (string)x.Element("Banco") ?? string.Empty,
                              CBU = (string)x.Element("CBU") ?? string.Empty
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de transferencias", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de transferencias. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar transferencias desde XML", ex);
            }
        }

        public BETransferencia BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(t => t.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de transferencias", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de transferencias. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar transferencia por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var ids = doc.Descendants("Transferencia")
                             .Select(x => (long?)x.Attribute("Id") ?? 0L);
                return ids.Any() ? ids.Max() : 0L;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de transferencias", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de transferencias. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception)
            {
                return 0L;
            }
        }

        #endregion
    }
}
