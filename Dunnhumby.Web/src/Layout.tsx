import Header from "@/components/Header";
import { Outlet } from "react-router-dom";
import Container from "@/components/Container";
import { RoutingProps } from "@/Routing";

interface Routing {
  routing: RoutingProps[];
}

export default function Layout({ routing }: Routing) {
  return (
    <>
      <Header routing={routing} />
      <Container>
        <Outlet />
      </Container>
    </>
  );
}
