import { Product, StockPerCategory } from "@/models/Product";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

const { VITE_PUBLIC_API_URL } = import.meta.env;
const getKey = "getProduct";

export const useFetchProducts = () => {
  return useQuery({
    queryKey: [getKey],
    queryFn: async () => {
      const response = await axios.get(`${VITE_PUBLIC_API_URL}/products`);
      const data: Product[] = await response.data;
      return data;
    },
  });
};

export const useFetchStockQuantityPerCategory = () => {
  return useQuery({
    queryKey: [getKey],
    queryFn: async () => {
      const response = await axios.get(
        `${VITE_PUBLIC_API_URL}/products/stockcategory`
      );
      const data: StockPerCategory[] = await response.data;
      return data;
    },
  });
};

export const useCreateProduct = () => {
  const client = useQueryClient();
  const navigate = useNavigate();
  return useMutation({
    mutationFn: async (product: Product) => {
      const response: AxiosResponse<string> = await axios.post(
        `${VITE_PUBLIC_API_URL}/products`,
        product,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      return response.data;
    },
    onSuccess: async () => {
      await client.invalidateQueries({
        queryKey: [getKey],
      });
      toast.success("New Product Created!");
      navigate("/");
    },
    onError: (error) => {
      toast.error(error.message || "An unexpected error occurred");
    },
  });
};
