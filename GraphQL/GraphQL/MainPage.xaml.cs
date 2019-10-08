using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Models;
using GraphQL.Responses;
using GraphQL.Services;
using Xamarin.Forms;

namespace GraphQL
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly IGraphQLService _graphQLService;

        public MainPage()
        {
            InitializeComponent();
            _graphQLService = new GraphQLClientService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var graphQuery = "query{ user(login: marcofolio){    name    bio    company    location    followers(first: 10)    {      nodes      {        id        name      }    }  } }";
            var user = await _graphQLService.Query<User>("https://api.github.com/graphql", graphQuery);

            Name.Text = user.name;
            Bio.Text = user.bio.ToString();
            Followers.ItemsSource = user.followers.nodes.Where(u => !string.IsNullOrEmpty(u.name)).Select(u => u.name);
        }
    }
}
