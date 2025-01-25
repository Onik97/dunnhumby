import Container from "@/components/Container";
import ErrorDisplay from "@/components/ErrorDisplay";
import LoadingDisplay from "@/components/LoadingDisplay";
import ProductQuantityPerCategoryGraph from "@/components/products/ProductQuantityPerCategoryGraph";
import { useFetchStockQuantityPerCategory } from "@/hooks/ProductHooks";

export default function StockPerCategoryPage() {
  const { data, isLoading, error } = useFetchStockQuantityPerCategory();
  return (
    <Container>
      {isLoading && <LoadingDisplay />}
      {error && <ErrorDisplay error={error} />}
      {data && <ProductQuantityPerCategoryGraph data={data} />}
    </Container>
  );
}
