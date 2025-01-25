import Container from "@/components/Container";
import { NavLink } from "react-router-dom";

export default function ErrorPage() {
  return (
    <Container>
      <h1>Page not found</h1>
      <p>We looked everywhere but it must have got away.</p>
      <button>
        <NavLink to="/">Home</NavLink>
      </button>
    </Container>
  );
}
