import { Landing } from "./components/Landing";
import Login, { LogNavigate } from "./components/Login";
import Register, { RegNavigate } from "./components/Register";
import { Home } from "./components/Home";

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
    path: '/Home',
    element: <Home />
  },
];

export default AppRoutes;
