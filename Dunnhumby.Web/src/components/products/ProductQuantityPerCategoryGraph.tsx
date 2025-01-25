import { Bar, BarChart, CartesianGrid, XAxis } from "recharts";
import {
  ChartConfig,
  ChartContainer,
  ChartTooltip,
  ChartTooltipContent,
} from "@/components/ui/chart";
import { StockPerCategory } from "@/models/Product";

const chartConfig = {
  totalStock: {
    label: "Total",
    color: "#2563eb",
  },
} satisfies ChartConfig;

interface ProductQuantityPerCategoryGraphProps {
  data: StockPerCategory[];
}

export default function ProductQuantityPerCategoryGraph({
  data,
}: ProductQuantityPerCategoryGraphProps) {
  return (
    <ChartContainer config={chartConfig}>
      <BarChart accessibilityLayer data={data}>
        <CartesianGrid vertical={false} />
        <XAxis
          dataKey="category"
          tickLine={true}
          tickMargin={10}
          axisLine={true}
          tickFormatter={(value) => value}
        />
        <ChartTooltip content={<ChartTooltipContent />} />
        <Bar dataKey="totalStock" fill="var(--color-quantity)" radius={4} />
      </BarChart>
    </ChartContainer>
  );
}
