import { Landing } from "./components/Landing";
import Login, { LogNavigate } from "./components/Login";
import Register, {RegNavigate} from "./components/Register";

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
];

export default AppRoutes;
