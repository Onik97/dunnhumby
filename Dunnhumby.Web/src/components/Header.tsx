import { NavLink } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet";
import { Menu } from "lucide-react";
import { Routing } from "@/Main";
import Container from "./Container";

export default function Header({ routing }: Routing) {
  return (
    <Container>
      <header className="sm:flex sm:justify-between py-3 px-4 border-b">
        <div className="relative px-4 sm:px-6 lg:px-8 flex h-16 items-center justify-between w-full">
          <div className="flex items-center">
            <Sheet>
              <SheetTrigger>
                <Menu className="h-6 md:hidden w-6" />
              </SheetTrigger>
              <SheetContent
                side="left"
                className="w-[300px] sm:w-[400px]"
                asChild
              >
                <nav className="flex flex-col gap-4">
                  {routing
                    .filter((r) => r.navRouting)
                    .map((route, i) => (
                      <NavLink
                        key={i}
                        to={route.route.path!}
                        className="block px-2 py-1 text-lg"
                      >
                        {route.label}
                      </NavLink>
                    ))}
                </nav>
              </SheetContent>
            </Sheet>
            <NavLink to="/" className="ml-4 lg:ml-0">
              <h1 className="text-xl font-bold">Dunnhumby</h1>
            </NavLink>
          </div>
          <nav className="mx-6 flex items-center space-x-4 lg:space-x-6 hidden md:block">
            {routing
              .filter((r) => r.navRouting)
              .map((route, i) => (
                <Button asChild variant="ghost">
                  <NavLink
                    key={i}
                    to={route.route.path!}
                    className="text-sm font-medium transition-colors"
                  >
                    {route.label}
                  </NavLink>
                </Button>
              ))}
          </nav>
        </div>
      </header>
    </Container>
  );
}
