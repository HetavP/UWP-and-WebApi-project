using ProjectA_B_UWP.Data;
using ProjectA_B_UWP.Models;
using ProjectA_B_UWP.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectA_B_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ISportRepository sportRepository;
        private readonly IContingentRepository contingentRepository;
        private readonly IAthleteRepository athleteRepository;

        public MainPage()
        {
            InitializeComponent();
            sportRepository = new SportRepository();
            contingentRepository = new ContingentRepository();
            athleteRepository = new AthleteRepository();
            FillSportDropDown();
            FillContingentDropDown();
        }

        private async void FillSportDropDown()
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Sport> sports = await sportRepository.GetSports();
                //Add the All Option
                sports.Insert(0, new Sport { ID = 0, Name = " - All Sports" });
                //Bind to the ComboBox
                SportCombo.ItemsSource = sports;
                btnAdd.IsEnabled = true;
                ShowSportAthletes(null);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "No Athlete play that sport.");
                    FillSportDropDown();

                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void SportCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sport selSport = (Sport)SportCombo.SelectedItem;
            ShowSportAthletes(selSport?.ID);
        }

        private async void FillContingentDropDown()
        {
            // Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Contingent> contingents = await contingentRepository.GetContingents();
                // Add the All Option
                contingents.Insert(0, new Contingent { ID = 0, Name = " - All Contingents" });
                // Bind to the ComboBox
                ContingentCombo.ItemsSource = contingents;
                btnAdd.IsEnabled = true;
                ShowAthletesByContingent(null);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "No Athlete from that contingent.");
                    FillContingentDropDown();
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void ContingentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contingent selContingent = (Contingent)ContingentCombo.SelectedItem;
            ShowAthletesByContingent(selContingent?.ID);
        }

        private async void ShowAthletesByContingent(int? ContingentID)
        {
            // Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Athlete> athletes;
                if (ContingentID.GetValueOrDefault() > 0)
                {
                    athletes = await athleteRepository.GetAthletesByContingent(ContingentID.GetValueOrDefault());
                }
                else
                {
                    athletes = await athleteRepository.GetAthletes();
                }
                athleteList.ItemsSource = athletes;
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "No Athlete from that contingent.");
                    FillContingentDropDown();

                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private async void ShowSportAthletes(int? SportID)
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                {
                    List<Athlete> athletes;
                    if (SportID.GetValueOrDefault() > 0)
                    {
                        athletes = await athleteRepository.GetAthletesBySport(SportID.GetValueOrDefault());
                    }
                    else
                    {
                        athletes = await athleteRepository.GetAthletes();
                    }
                    athleteList.ItemsSource = athletes;

                }
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", " No Athlete play that sport.");
                    FillSportDropDown();
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void athleteGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the detail page
            Frame.Navigate(typeof(AthleteDetailPage), (Athlete)e.ClickedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Athlete newAth = new Athlete();
            newAth.DOB = DateTime.Now;

            // Navigate to the detail page
            Frame.Navigate(typeof(AthleteDetailPage), newAth);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            FillSportDropDown();
            FillContingentDropDown();   
        }
    }
}
