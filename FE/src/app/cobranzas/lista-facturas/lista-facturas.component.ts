import { Component, OnDestroy, OnInit } from '@angular/core';
import { Localidad } from 'src/interfaces/localidad';
import { Provincia } from 'src/interfaces/provincia';
import { LocalidadProvider } from 'src/providers/localidadProvider';
import { ProvinciaProvider } from 'src/providers/provinciaProvider';

@Component({
  selector: 'app-lista-facturas',
  templateUrl: './lista-facturas.component.html',
  styleUrls: ['./lista-facturas.component.css']
})
export class ListaFacturasComponent implements OnInit {

  listaLocalidades: Localidad[];
  listaProvincias: Provincia[];
  
  constructor(private provinciaApi: ProvinciaProvider,
    private localidadApi: LocalidadProvider,) { }

    ngOnInit(): void {
      this.obtenerProvincias();
    }
  
  
  
    obtenerProvincias() {
      this.provinciaApi.ObtenerTodas().subscribe((data) => {
        if (data) {
          this.listaProvincias = data;
          console.log(this.listaProvincias);
        } else {
          alert(data);
        }
      })
    }
  
    obtenerLocalidad(id: any) {
      this.localidadApi.ObtenerPorId(id).subscribe((data) => {
        if (data.ok) {
          this.listaLocalidades = data.listaLocalidades;
        } else {
          alert(data.error);
        }
      })
    }
  
}
