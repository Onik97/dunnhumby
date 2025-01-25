import ReactDOM from "react-dom/client";
import {
  RouteObject,
  RouterProvider,
  createBrowserRouter,
} from "react-router-dom";
import ErrorPage from "@/pages/ErrorPage";
import "./index.css";
import Layout from "./Layout";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "@/components/ui/sonner";
import CreateProductPage from "./pages/Product/CreateProductPage";
import HomePage from "@/pages/HomePage";
import StockPerCategoryPage from "./pages/Product/StockPerCategoryPage";

export interface Routing {
  routing: RoutingProps[];
}

interface RoutingProps {
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

const router = createBrowserRouter([
  {
    element: <Layout routing={routesWithLabels} />,
    children: routesWithLabels.map((route) => route.route),
  },
]);

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById("root")!).render(
  <QueryClientProvider client={queryClient}>
    <Toaster />
    <RouterProvider router={router}></RouterProvider>
  </QueryClientProvider>
);
