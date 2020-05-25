/*
 * MIT License
 *
 * Copyright (c) 2020 Diego Urrutia-Astorga <durrutia@ucn.cl>.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

// https://doc.zeroc.com/ice/3.7/language-mappings/java-mapping/client-side-slice-to-java-mapping/customizing-the-java-mapping
["java:package:cl.ucn.disc.pdis.fivet.zeroice"]
module model {

    /**
    * Clase persona (Duenio)
    */

        class Persona{


            /**
            * PK
            */
            int id;

            /**
            *Rut: 18124996K
            */
            string rut;

            /**
            * Nombre;
            */
            string nombre;

            /**
            * Direccion
            */
            string direccion;

            /**
            * Telefono fijo: +56 55 23550000
            */
            long telefonoFijo;

            /**
            * Telefono movil
            */
            long telefonoMovil;

            /**
            *Correo Electronico
            */
            string email;

        }

            /**
            *The Sexo
            */

            enum Sexo {
                 MACHO,
                 HEMBRA
            }

            /**
            * TipoPaciente
            */

            enum TipoPaciente {
                  INTERNO,
                  EXTERNO
            }

     /**
     *Clase Ficha
     */
        class Ficha {

                /**
                *PrimaryKey
                */
                int id;

                /**
                * Numero 2345;
                **/
                int numero;

                /**
                 * Nombre: Copito.
                 */
                 string nombre;

                 /**
                 * Fecha de Nacimiento.
                 *Format: ISO_ZONED_DATE_TIME
                 */
                  string fechaNacimiento;

                  /**
                  * Raza
                  */
                  string raza;


                   /**
                   *color
                   */
                   string color;

                   /**
                   * Sexo : Macho /Hembra
                   */
                    Sexo sexo;

                    /**
                    * Tipo Paciente: interno / externo
                    */
                     TipoPaciente tipoPaciente;

                //Nota de clase: Foto puede ser un atributo de ficha. Puede ser una secuencia
                //de string.

                }


            /**
            *Clase Control
            */

                class Control{

                 /**
                 *PK
                 */
                 int id;

                  /**
                  * Fecha:
                  */
                  string fecha;

                  /**
                  * Fecha del proximo control
                  *Format: ISO_ZONED_DATE_TIME
                  */
                  string fechaProxControl;

                  /**
                  *Temperatura
                  */
                  double temperatura;

                  /**
                  *Peso
                  */
                  double peso;

                  /**
                  *Altura
                  */
                  double altura;

                  /**
                  *Diagnostico
                  */
                  string diagnostico;


              }

              /**
              * The Contratos.
              */

              interface Contratos{

                /**
                 * Deseo registrar los datos de un paciente.
                 *
                 * @param ficha a crear en el backend.
                 * @return the ficha almacenada en el backend (con numero asignado).
                 */
                 Ficha crearFicha(Ficha ficha);

                 /**
                 * Deseo registrar los datos del duenio de un paciente.
                 *
                 * @param persona a crear en el backend.
                 * @return the Persona almacenada en el backend.
                 */
                 Persona crearPersona(Persona persona);

                 /**
                 * Deseo registrar los datos de un Control.
                 *
                 * @param numeroFicha al cual sera asignado el control.
                 * @param control a agregar.
                 */
                 Control crearControl(int numeroFicha, Control control);

                 /**
                 *Dado un numero de ficha,retorna la ficha asociada
                 *
                 *@param numero de ficha a obtener
                 *@return The Ficha
                 */
                 Ficha obtenerFicha(int numero);


                 /**
                 * Dado un numero de rut retorna la persona
                 *
                 * @param numero de ficha a obtener
                 * @return The Persona
                 */
                 Persona ObtenerPersona(string rut);

              }

    /**
     * The base system.
     */
     interface TheSystem {

        /**
         * @return the diference in time between client and server.
         */
        long getDelay(long clientTime);

     }

}



