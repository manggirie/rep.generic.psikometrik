﻿/// <reference path="../durandal/amd/require.js" />

define([],
    function () {
        var cultures = {
            building: {
                TEMPLATE_NAME: "Templat bangunan",
                SAVE_BUILDING_MESSAGE: "Bangunan sudah di simpan",
                MAP_TITLE: "Peta bangunan"
            },
            space: {
                title: "Senarai ruang",
                ADD_NEW_BUILDING: "[Tiada dalam senarai]",
                toolbar: {
                    ADD_NEW_SPACE: "Tambah ruang baru"
                }
            },
            spacetemplate: {
                title: "Templat ruang"
            },
            messages: {
                SAVE_SUCCESS: "{0} sudah berjaya di simpan",
                SAVE_ERROR: "Ada masalah untuk menyimpan data anda",
                FORM_IS_NOT_VALID: "Sila pastikan input anda betul",
                DELETE_SUCCESS: "{0} sudah berjaya dihapuskan",
            },
            lots: {
                LOT_LIST_TITLE: "Senarai unit di {0}"
            },
            maintenance_detail: {
                CLOSE_MAINTENANCE_BUTTON_CAPTION: "Penyenggaraan Selesai",
                ASSIGN_MAINTENANCE_BUTTON_CAPTION: "Tugaskan Pegawai"
            },
            maintenance: {
                SAVE_TEMPLATE: "Templat berjaya disimpan",
                NEW_MAINTENANCE_STATUS_CAPTION: "Baru",
                INSPECTION_MAINTENANCE_STATUS_CAPTION: "Pemeriksaan",
                INPROGRESS_MAINTENANCE_STATUS_CAPTION: "Penyenggaraan",
                DONE_MAINTENANCE_STATUS_CAPTION: "Selesai"

            },
            application: {
                TEMPLATE_NAME: "Templat Permohonan"
            }
        };

        return cultures;

    });
