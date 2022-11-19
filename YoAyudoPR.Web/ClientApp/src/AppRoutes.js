import { Home } from "./components/Home";
import Login, { LogNavigate } from "./components/Login";
import Register, {RegNavigate} from "./components/Register";

const AppRoutes = [
  {
    index: true,
    element: <Home />
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
