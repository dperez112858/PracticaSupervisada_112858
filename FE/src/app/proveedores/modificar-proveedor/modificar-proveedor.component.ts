import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProveedorProvider } from 'src/providers/proveedorProvider';

@Component({
  selector: 'app-modificar-proveedor',
  templateUrl: './modificar-proveedor.component.html',
  styleUrls: ['./modificar-proveedor.component.css']
})
export class ModificarProveedorComponent implements OnInit {

  idProveedor: string = "";

  constructor(private proveedorApi: ProveedorProvider, private route: ActivatedRoute) {
    this.idProveedor = this.route.snapshot.params["id"];
    alert(this.idProveedor);
   }
  //ActivatedRoute me permite recibir parametros por medio de la url

  ngOnInit(): void {
  }

}
