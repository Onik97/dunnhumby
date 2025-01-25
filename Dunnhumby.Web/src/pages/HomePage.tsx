import Container from "@/components/Container";
import ErrorDisplay from "@/components/ErrorDisplay";
import LoadingDisplay from "@/components/LoadingDisplay";
import ProductDataTable from "@/components/products/ProductDataTable";
import { columns } from "@/components/products/ProductDataTableColumnDefinition";
import { useFetchProducts } from "@/hooks/ProductHooks";

export default function HomePage() {
  const { data, isLoading, error } = useFetchProducts();
  return (
    <Container>
      {isLoading && <LoadingDisplay />}
      {error && <ErrorDisplay error={error} />}
      {data && <ProductDataTable columns={columns} data={data} />}
    </Container>
  );
}
