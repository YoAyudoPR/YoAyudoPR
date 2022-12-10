import { Routes, Route, Link, Navigate } from 'react-router-dom';
import { Landing } from "./components/Landing";
import Login, { LogNavigate, LogInStatus } from "./components/Login";
import Register, { RegNavigate } from "./components/Register";
import { Home, HomeNavigate } from "./components/Home";
import EventDetails, { EventDetailsNavigate } from "./components/EventDetails"; 
import CreateEvent, { CreateEventNavigate } from "./components/CreateEvent";
import Profile, { ProfileNavigate } from "./components/Profile";

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
     element: <HomeNavigate/> /*<ProtectedRoute auth={{ isAuthenticated: { LogInStatus } }}> <Home/></ProtectedRoute>*/
  },
];

export default AppRoutes;
