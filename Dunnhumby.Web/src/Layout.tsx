import Header from "@/components/Header";
import { Outlet } from "react-router-dom";
import { Routing } from "./Main";

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
