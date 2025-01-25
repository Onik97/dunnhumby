import { RouteObject, createBrowserRouter } from "react-router-dom";
import ErrorPage from "@/pages/ErrorPage";
import Layout from "./Layout";
import HomePage from "@/pages/HomePage";
import CreateProductPage from "./pages/Product/CreateProductPage";
import StockPerCategoryPage from "./pages/Product/StockPerCategoryPage";

export interface Routing {
  routing: RoutingProps[];
}

export interface RoutingProps {
  label?: string;
  navRouting?: boolean;
  profileRouting?: boolean;
  route: RouteObject;
}

const routesWithLabels: RoutingProps[] = [
  {
    label: "Home",
    route: {
      path: "/",
      element: <HomePage />,
    },
  },
  {
    label: "Create Product",
    navRouting: true,
    route: {
      path: "/product/create",
      element: <CreateProductPage />,
    },
  },
  {
    label: "Stock Per Category",
    navRouting: true,
    route: {
      path: "/product/stockcategory",
      element: <StockPerCategoryPage />,
    },
  },
  {
    route: {
      path: "*",
      element: <ErrorPage />,
    },
  },
];

export const router = createBrowserRouter([
  {
    element: <Layout routing={routesWithLabels} />,
    children: routesWithLabels.map((route) => route.route),
  },
]);
