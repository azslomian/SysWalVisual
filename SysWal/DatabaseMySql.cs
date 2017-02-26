using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWal
{
    class DatabaseMySql
    {
       /* public void UpdateWizyte(wizyta nowa_wizyta, wizyta stara_wizyta)
        {

            string commandString = "Update wizyta Set id_pacjenta = @id_pacjenta, "
                + "id_pracownika = @id_pracownika, data = @data, zalecenia = @zalecenia," +
                " diagnoza = @diagnoza where id_pracownika = @id_pracownika_stare, id_pacjenata = @id_pacjenta_stare," +
                " data = @data_stare, zalecenia = @zalecenia_stare, diagnoza = @diagnoza_stare";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@id_pacjenta", nowa_wizyta.id_pacjenta);
            myCommand.Parameters.AddWithValue("@id_pracownika", nowa_wizyta.id_pracownika);
            myCommand.Parameters.AddWithValue("@zalecenia", nowa_wizyta.zalecenia);
            myCommand.Parameters.AddWithValue("@data", nowa_wizyta.data);
            myCommand.Parameters.AddWithValue("@diagnoza", nowa_wizyta.diagnoza);
            myCommand.Parameters.AddWithValue("@id_pacjenta_stare", stara_wizyta.id_pacjenta);
            myCommand.Parameters.AddWithValue("@id_pracownika_stare", stara_wizyta.id_pracownika);
            myCommand.Parameters.AddWithValue("@zalecenia_stare", stara_wizyta.zalecenia);
            myCommand.Parameters.AddWithValue("@data_stare", stara_wizyta.data);
            myCommand.Parameters.AddWithValue("@diagnoza_stare", stara_wizyta.diagnoza);
            myCommand.ExecuteNonQuery();
            DBConnectionSingleton.CloseConnection();
        }



        public List<wizyta> WczytajListeWizytMySQL(int id_lekarza)
        {
            List<wizyta> ListaWizyt = new List<wizyta>();
            DateTime today = DateTime.Today;

            string commandString = "select id, id_pracownika, id_pacjenta, data from wizyta where wizyta.data > @dzisiaj,id_pracownika = @id_lekarza";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@dzisiaj", today);
            myCommand.Parameters.AddWithValue("@id_lekarza", id_lekarza);


            MySqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                wizyta nowaWizyta = new wizyta();
                nowaWizyta.id = (int)myReader["id"];
                nowaWizyta.id_pracownika = (int)(myReader["id_pracownika"]);
                nowaWizyta.id_pacjenta = (int)(myReader["id_pacjenta"]);
                nowaWizyta.data = DateTime.Parse((myReader["data"]).ToString());

                ListaWizyt.Add(nowaWizyta);
            }
            DBConnectionSingleton.CloseConnection();
            return ListaWizyt;
        }

        public List<wizyta> WczytajListeWizytMySQLDlaDnia(int id_lekarza, DateTime data)
        {
            List<wizyta> ListaWizyt = new List<wizyta>();
            DateTime today = DateTime.Today;
            var dataDzisiaj = data.Date;
            var dataJutro = data.AddDays(1.0);

            string commandString = "select id, id_pracownika, id_pacjenta, data from wizyta where data > @dataDzisiaj and data < @dataJutro and id_pracownika = @id_lekarza";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@dataDzisiaj", dataDzisiaj);
            myCommand.Parameters.AddWithValue("@dataJutro", dataJutro);
            myCommand.Parameters.AddWithValue("@id_lekarza", id_lekarza);


            MySqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                wizyta nowaWizyta = new wizyta();
                nowaWizyta.id = (int)myReader["id"];
                nowaWizyta.id_pracownika = (int)(myReader["id_pracownika"]);
                nowaWizyta.id_pacjenta = (int)(myReader["id_pacjenta"]);
                nowaWizyta.data = DateTime.Parse((myReader["data"]).ToString());

                ListaWizyt.Add(nowaWizyta);
            }
            DBConnectionSingleton.CloseConnection();
            return ListaWizyt;
        }


        public List<harmonogram_pracy> WczytajHarmonogramyLekarza(int id_lekarza)
        {
            List<harmonogram_pracy> ListaDziennychHarmonogramow = new List<harmonogram_pracy>();
            DateTime today = DateTime.Today;

            string commandString = "select id, id_pracownika, data, godzina_start, godzina_stop from harmonogram_pracy where id_pracownika = @id_lekarza and data > @dzisiaj";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@dzisiaj", today);
            myCommand.Parameters.AddWithValue("@id_lekarza", id_lekarza);


            MySqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                harmonogram_pracy nowyHarmonogram = new harmonogram_pracy();
                nowyHarmonogram.id = (int)myReader["id"];
                nowyHarmonogram.id_pracownika = (int)(myReader["id_pracownika"]);
                nowyHarmonogram.data = DateTime.Parse((myReader["data"]).ToString());
                nowyHarmonogram.godzina_start = TimeSpan.Parse((myReader["godzina_start"]).ToString());
                nowyHarmonogram.godzina_stop = TimeSpan.Parse((myReader["godzina_stop"]).ToString());

                ListaDziennychHarmonogramow.Add(nowyHarmonogram);
            }
            DBConnectionSingleton.CloseConnection();
            return ListaDziennychHarmonogramow;
        }


        public harmonogram_pracy WczytajHarmonogramDniaLekarza(int id_lekarza, DateTime data)
        {

            var dataDnia = data.Date;
            string commandString = "select id, id_pracownika, data, godzina_start, godzina_stop from harmonogram_pracy where id_pracownika = @id_lekarza and data = @data";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@data", dataDnia);
            myCommand.Parameters.AddWithValue("@id_lekarza", id_lekarza);

            harmonogram_pracy harmonogram = null;
            MySqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                harmonogram = new harmonogram_pracy();
                harmonogram.id = (int)myReader["id"];
                harmonogram.id_pracownika = (int)(myReader["id_pracownika"]);
                harmonogram.data = DateTime.Parse((myReader["data"]).ToString());
                harmonogram.godzina_start = TimeSpan.Parse((myReader["godzina_start"]).ToString());
                harmonogram.godzina_stop = TimeSpan.Parse((myReader["godzina_stop"]).ToString());
            }
            DBConnectionSingleton.CloseConnection();
            return harmonogram;
        }


        public void ZapiszWizyte(wizyta nowa_wizyta)
        {
            string commandString = "INSERT INTO wizyta(id_pacjenta, id_pracownika, zalecenia, diagnoza, data) values (@id_pacjenta, @id_pracownika, @zalecenia, @diagnoza, @data)";
            SqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;

            myCommand.Parameters.AddWithValue("@id_pacjenta", nowa_wizyta.id_pacjenta);
            myCommand.Parameters.AddWithValue("@id_pracownika", nowa_wizyta.id_pracownika);
            myCommand.Parameters.AddWithValue("@zalecenia", DBNull.Value);
            myCommand.Parameters.AddWithValue("@diagnoza", DBNull.Value);
            myCommand.Parameters.AddWithValue("@data", nowa_wizyta.data);
            myCommand.ExecuteNonQuery();
            DBConnectionSingleton.CloseConnection();
        }

        public void ZapiszHarmonogram(harmonogram_pracy harmonogram)
        {
            string commandString = "INSERT INTO harmonogram_pracy(id_pracownika, data, godzina_start, godzina_stop) values (@id_lekarza, @data, @godzina_start, @godzina_stop)";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@id_lekarza", harmonogram.id_pracownika);
            myCommand.Parameters.AddWithValue("@data", harmonogram.data);
            myCommand.Parameters.AddWithValue("@godzina_start", harmonogram.godzina_start);
            myCommand.Parameters.AddWithValue("@godzina_stop", harmonogram.godzina_stop);
            myCommand.ExecuteNonQuery();
            DBConnectionSingleton.CloseConnection();

        }

        public void UpdateDoHarmonogramu(harmonogram_pracy nowyHarmonogram)
        {
            string commandString = "Update harmonogram_pracy Set godzina_start = @godzina_start, godzina_stop = @godzina_stop where id_pracownika = @id_lekarza and data = @data ";
            MySqlCommand myCommand = new MySqlCommand(commandString, DBConnectionSingleton.getConnection());
            myCommand.CommandText = commandString;
            myCommand.Parameters.AddWithValue("@id_lekarza", nowyHarmonogram.id_pracownika);
            myCommand.Parameters.AddWithValue("@data", nowyHarmonogram.data);
            myCommand.Parameters.AddWithValue("@godzina_start", nowyHarmonogram.godzina_start);
            myCommand.Parameters.AddWithValue("@godzina_stop", nowyHarmonogram.godzina_stop);
            myCommand.ExecuteNonQuery();
            DBConnectionSingleton.CloseConnection();
        }*/
    }
}
