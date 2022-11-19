import { Home } from "./components/Home";
import { Login } from "./components/Login";
import Register, {WithNavigate} from "./components/Register";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/Login',
    element: <Login />
  },
  {
    path: '/Register',
    element: <WithNavigate /> 
  },
];

export default AppRoutes;
