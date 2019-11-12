using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creating an instance of entities which gives access to EF's features
        DbEntities db = new DbEntities();
        // Creating an instance of Spillere (database) to use later with EF's features
        Spillere players = new Spillere();
        // Creating an instance of Turneringer (database) to use later with EF's features
        Turneringer tournaments = new Turneringer();
        // Creating an instance of Turneringer (database) to use later with EF's features
        Ansatte employees = new Ansatte();

        public MainWindow()
        {
            InitializeComponent();
        }


        // Players - Click eventhandlers
        private void BtnCreatePlayer_Click(object sender, RoutedEventArgs e)
        {
            CreatePlayer();
        }

        private void BtnUpdatePlayer_Click(object sender, RoutedEventArgs e)
        {
            UpdatePlayer();
        }

        private void BtnDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            DeletePlayer();
        }

        private void BtnReadAllPlayers_Click(object sender, RoutedEventArgs e)
        {
            ReadPlayers();
        }

        private void BtnReadPlayersWithSponsorAndTournaments_Click(object sender, RoutedEventArgs e)
        {
            // ReadPlayersWithSponsorAndTournaments(); Doesn't work yet
        }


        // Tournaments - Click eventhandlers
        private void BtnCreateTournament_Click(object sender, RoutedEventArgs e)
        {
            CreateTournament();
        }

        private void BtnUpdateTournament_Click(object sender, RoutedEventArgs e)
        {
            UpdateTournament();
        }

        private void BtnDeleteTournament_Click(object sender, RoutedEventArgs e)
        {
            DeleteTournament();
        }

        private void BtnReadAllTournaments_Click(object sender, RoutedEventArgs e)
        {
            ReadTournaments();
        }

        private void BtnReadAllTournamentsWithTypesAndRegisteredPlayers_Click(object sender, RoutedEventArgs e)
        {
            // ReadTournamentsWithTypesAndRegisteredPlayers() Doesn't work yet
        }

        private void BtnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee();
        }

        private void BtnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployees();
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployee();
        }

        private void BtnReadAllEmployees_Click(object sender, RoutedEventArgs e)
        {
            ReadEmployees();
        }

        // Methods for Players click eventhandlers

        // Creates a new player (adds it to the datagrid) with values equal to the user's input and reloads it
        private void CreatePlayer()
        {
            txtPlayerID.Text = "";
            players.Navn = txtPlayername.Text;
            players.InGameNavn = txtIngameName.Text;
            players.Rang = cbxRank.Text;
            players.Telefonnummer = txtPlayerPhonenumber.Text;
            players.Turneringstype = cbxTournamenttype.Text;

            // Checks if any of the required fields are empty and if not adds the new player and saves the changes
            if (txtPlayername.Text != "" && txtIngameName.Text != "" && cbxRank.Text != "" && txtPlayerPhonenumber.Text != "" && cbxTournamenttype.Text != "")
            {
                db.Spilleres.Add(players);
                db.SaveChanges();
                ReadPlayers();
                Clear();
            }
            // If any of the required fields are empty creates a pop-up that describes the error
            else
            {
                MessageBox.Show("Fejl: Du har ikke udfyldt alle påkrævede felter.");
            }
        }

        // Reloads the datagrid
        private void ReadPlayers()
        {
            try
            {
                dgPlayers.ItemsSource = db.Spilleres.ToList();
            }
            catch
            {

            }
            Clear();
        }

        // If the ID field is filled and is matching an existing player's ID, then it's values will be overwritten by the user's inputs
        private void UpdatePlayer()
        {
            if (txtPlayername.Text == "" && txtIngameName.Text == "" && cbxRank.Text == "" && txtPlayerPhonenumber.Text == "" && cbxTournamenttype.Text == "")
            {
                if (txtPlayerID.Text != "")
                {
                    int updateID = Convert.ToInt32(txtPlayerID.Text);
                    var update = db.Spilleres.Where(w => w.SpillerID == updateID).FirstOrDefault();
                    txtPlayername.Text = update.Navn;
                    txtIngameName.Text = update.InGameNavn;
                    cbxRank.Text = update.Rang;
                    txtPlayerPhonenumber.Text = update.Telefonnummer;
                    cbxTournamenttype.Text = update.Turneringstype;
                    db.SaveChanges();
                    ReadPlayers();
                }
                else
                {
                    MessageBox.Show("Fejl: Du har ikke skrevet noget i ID feltet.");
                }
            }
            else
            {
                MessageBox.Show("Fejl: Du har ikke udfyldt alle påkrævede felter.");
            }
            Clear();
        }

        // If the ID field is filled and is matching an existing player's ID, then it is deleted
        private void DeletePlayer()
        {
            if (txtPlayerID.Text != "")
            {
                int deleteID = Convert.ToInt32(txtPlayerID.Text);
                var delete = db.Spilleres.Where(w => w.SpillerID == deleteID).FirstOrDefault();
                if (delete != null)
                {
                    db.Spilleres.Remove(delete);
                    db.SaveChanges();
                    ReadPlayers();
                }
                else
                {
                    MessageBox.Show("Fejl: Spiller med det indtastede ID eksisterer ikke.");
                }
            }
            Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------


        // Methods for Tournaments click eventhandlers

        // Creates a new tournament (adds it to the datagrid) with values equal to the user's input and reloads it
        private void CreateTournament()
        {
            txtTournamentID.Text = "";
            tournaments.Turneringsnavn = txtTournamentname.Text;
            tournaments.SpillerID = Convert.ToInt32(txtTournamentPlayerID.Text);
            tournaments.DommerID = Convert.ToInt32(txtJudgeID.Text);

            // Checks if any of the required fields are empty and if not adds the new player and saves the changes
            if (txtTournamentname.Text != "" && txtTournamentPlayerID.Text != "" && txtJudgeID.Text != "")
            {
                db.Turneringers.Add(tournaments);
                db.SaveChanges();
                ReadTournaments();
            }
            // If any of the required fields are empty creates a pop-up that describes the error
            else
            {
                MessageBox.Show("Fejl: Du har ikke udfyldt alle påkrævede felter.");
            }
            Clear();
        }

        // Reloads the datagrid
        private void ReadTournaments()
        {
            try
            {
                dgTournaments.ItemsSource = db.Turneringers.ToList();
            }
            catch
            {

            }
            Clear();
        }

        // If the ID field is filled and is matching an existing tournament's ID, then it's values will be overwritten by the user's inputs
        private void UpdateTournament()
        {
            if (txtTournamentname.Text == "" && txtTournamentPlayerID.Text == "" && txtJudgeID.Text == "")
            {
                if (txtTournamentID.Text != "")
                {
                    int updateID = Convert.ToInt32(txtTournamentID.Text);
                    var update = db.Turneringers.Where(w => w.TurneringsID == updateID).FirstOrDefault();
                    txtTournamentname.Text = update.Turneringsnavn;
                    txtTournamentPlayerID.Text = Convert.ToString(update.SpillerID);
                    txtJudgeID.Text = Convert.ToString(update.DommerID);
                    db.SaveChanges();
                    ReadTournaments();
                }
                else
                {
                    MessageBox.Show("Fejl: Du har ikke skrevet noget i ID feltet.");
                }
            }
            else
            {
                MessageBox.Show("Fejl: Du har ikke udfyldt alle påkrævede felter.");
            }
            Clear();
        }

        // If the ID field is filled and is matching an existing player's ID, then it is deleted
        private void DeleteTournament()
        {
            if (txtTournamentID.Text != "")
            {
                int deleteID = Convert.ToInt32(txtPlayerID.Text);
                var delete = db.Turneringers.Where(w => w.TurneringsID == deleteID).FirstOrDefault();
                if (delete != null)
                {
                    db.Turneringers.Remove(delete);
                    db.SaveChanges();
                    ReadTournaments();
                }
                else
                {
                    MessageBox.Show("Fejl: Turnering med det indtastede ID eksisterer ikke.");
                }
            }
            Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------


        // Methods for Tournaments click eventhandlers

        // Creates a new tournament (adds it to the datagrid) with values equal to the user's input and reloads it
        private void CreateEmployee()
        {
            txtEmployeeID.Text = "";
            employees.Navn = txtEmployeename.Text;
            employees.Telefonnummer = txtEmployeePhonenumber.Text;
            employees.Løn = txtSalary.Text;
            employees.Jobtype = cbxJobtype.Text;
            if (cbxJobtype.Text == "Dommer" && cbxJudgeLevel.Text != "")
            {
                employees.DommerLevel = Convert.ToInt32(cbxJudgeLevel.Text);
            }
            else
            {
                employees.DommerLevel = 0;
            }

            // Checks if any of the required fields are empty and if not adds the new player and saves the changes
            if (txtEmployeename.Text != "" && txtEmployeePhonenumber.Text != "" && txtSalary.Text != "" && cbxJobtype.Text != "")
            {
                db.Ansattes.Add(employees);
                db.SaveChanges();
                ReadEmployees();
            }
            // If any of the required fields are empty creates a pop-up that describes the error
            else
            {
                MessageBox.Show("Fejl: Du har ikke udfyldt alle påkrævede felter.");
            }
            Clear();
        }

        // Reloads the datagrid
        private void ReadEmployees()
        {
            try
            {
                dgEmployees.ItemsSource = db.Ansattes.ToList();
            }
            catch
            {

            }
            Clear();
        }

        // If the ID field is filled and is matching an existing tournament's ID, then it's values will be overwritten by the user's inputs
        private void UpdateEmployees()
        {
            if (txtEmployeename.Text == "" && txtEmployeePhonenumber.Text == "" && txtSalary.Text == "" && cbxJobtype.Text != "")
            {
                if (txtEmployeeID.Text != "")
                {
                    int updateID = Convert.ToInt32(txtTournamentID.Text);
                    var update = db.Ansattes.Where(w => w.MedarbejderID == updateID).FirstOrDefault();
                    txtEmployeename.Text = update.Navn;
                    txtEmployeePhonenumber.Text = update.Telefonnummer;
                    txtSalary.Text = Convert.ToString(update.Løn);
                    db.SaveChanges();
                    ReadEmployees();
                }
                else
                {
                    MessageBox.Show("Fejl: Du har ikke skrevet noget i ID feltet.");
                }
            }
            else
            {
                MessageBox.Show("Fejl: Du har ikke udfyldt alle påkrævede felter.");
            }
            Clear();
        }

        // If the ID field is filled and is matching an existing player's ID, then it is deleted
        private void DeleteEmployee()
        {
            if (txtEmployeeID.Text != "")
            {
                int deleteID = Convert.ToInt32(txtEmployeeID.Text);
                var delete = db.Ansattes.Where(w => w.MedarbejderID == deleteID).FirstOrDefault();
                if (delete != null)
                {
                    db.Ansattes.Remove(delete);
                    db.SaveChanges();
                    ReadEmployees();
                }
                else
                {
                    MessageBox.Show("Fejl: Turnering med det indtastede ID eksisterer ikke.");
                }
            }
            Clear();
        }

        // Clears all inputs in all tabs
        private void Clear()
        {
            txtPlayername.Text = "";
            txtIngameName.Text = "";
            cbxRank.Text = "";
            txtPlayerPhonenumber.Text = "";
            cbxTournamenttype.Text = "";
            cbxRegion.Text = "";
            txtPlayerID.Text = "";
            txtTournamentname.Text = "";
            txtTournamentPlayerID.Text = "";
            txtJudgeID.Text = "";
            txtTournamentID.Text = "";
            txtEmployeename.Text = "";
            txtEmployeePhonenumber.Text = "";
            txtSalary.Text = "";
            cbxJobtype.Text = "";
            cbxJudgeLevel.Text = "";
            txtEmployeeID.Text = "";
            txtCompanyname.Text = "";
            txtBusiness.Text = "";
            txtSponsorPlayerID.Text = "";
            txtSponsoredAmount.Text = "";
            txtSponsorID.Text = "";
        }
    }
}
