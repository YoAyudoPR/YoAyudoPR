import { Landing } from "./components/Landing";
import Login, { LogNavigate } from "./components/Login";
import Register, { RegNavigate } from "./components/Register";
import { Home } from "./components/Home";
import EventDetails, { EventDetailsNavigate } from "./components/EventDetails"; 
import CreateEvent, { CreateEventNavigate } from "./components/CreateEvent";
import Profile, { ProfileNavigate } from "./components/Profile";

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
    element: <Home />
  },
];

export default AppRoutes;
