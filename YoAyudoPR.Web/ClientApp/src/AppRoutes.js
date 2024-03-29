import { Landing } from "./components/Landing";
import { LogNavigate, LogInStatus } from "./components/Login";
import { RegNavigate } from "./components/Register";
import { HomeUserDetails } from "./components/Home";
import { EventDetailsNavigate } from "./components/EventDetails";
import { CreateEventNavigate } from "./components/CreateEvent";
import { ProfileNavigate } from "./components/Profile";
import { CreateOrganizationNavigate } from "./components/CreateOrganization";
import { OrganizationProfileNavigate } from "./components/OrganizationProfile";
import { SwaggerNavigate } from './components/Swagger';
import { AddHoursNavigate } from './components/AddHours';
import { AddMemberNavigate } from './components/AddMember';
import { OrgEventDetailsNavigate } from "./components/OrgEventDetails";

const AppRoutes = [
  {
    index: true,
    element: <Landing />
  },
  {
    path: '/Login',
    element: <LogNavigate />
  },
  {
    path: '/Register',
    element: <RegNavigate /> 
  },
  {
    path: '/EventDetails',
    element: <EventDetailsNavigate />
  },
  {
    path: '/CreateEvent',
    element: <CreateEventNavigate />
  },
  {
    path: '/Profile',
    element: <ProfileNavigate />
  },
  {
     path: '/Home',
     element: <HomeUserDetails />
   },
  {
    path: '/CreateOrganization',
    element: <CreateOrganizationNavigate />
   },
  {
    path: '/OrganizationProfile',
    element: <OrganizationProfileNavigate />
   },
  {
    path: '/Swagger',
    element: < SwaggerNavigate />
    },
    {
      path: '/AddHours',
      element: <AddHoursNavigate />
    },
    {
        path: '/AddMember',
        element: <AddMemberNavigate />
    },
    {
        path: '/OrgEventDetails',
        element: <OrgEventDetailsNavigate />
    }
];

export default AppRoutes;
