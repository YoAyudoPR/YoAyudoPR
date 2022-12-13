import { Routes, Route, Link, Navigate } from 'react-router-dom';
import { Landing } from "./components/Landing";
import Login, { LogNavigate, LogInStatus } from "./components/Login";
import Register, { RegNavigate } from "./components/Register";
import { Home, HomeUserDetails } from "./components/Home";
import EventDetails, { EventDetailsNavigate } from "./components/EventDetails"; 
import CreateEvent, { CreateEventNavigate } from "./components/CreateEvent";
import Profile, { ProfileNavigate } from "./components/Profile";
import CreateOrganization, { CreateOrganizationNavigate } from "./components/CreateOrganization";
import OrganizationProfile, { OrganizationProfileNavigate } from "./components/OrganizationProfile";
import Swagger, { SwaggerNavigate } from './components/Swagger';
import { AddHoursNavigate } from './components/AddHours';
import { AddMemberNavigate } from './components/AddMember';

const ProtectedRoute = ({ auth: { isAuthenticated }, children }) => {
    return isAuthenticated ? children : <Navigate to="/Login" />;
};

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
     element: <HomeUserDetails /> /*<ProtectedRoute auth={{ isAuthenticated: { LogInStatus } }}> <Home/></ProtectedRoute>*/
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
    }, {
      path: '/AddHours',
      element: <AddHoursNavigate />
    },
    {
        path: '/AddMember',
        element: <AddMemberNavigate />
    }
];

export default AppRoutes;
